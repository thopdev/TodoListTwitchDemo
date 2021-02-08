using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor.Services
{
    public class HttpService : IHttpService
    {
        private const string NoInternet =
            "Could not make a connection to the backend, make sure your internet connection is stable";

        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;


        public HttpService(HttpClient httpClient, IToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(_httpClient.BaseAddress + url);
                if (response.IsSuccessStatusCode)
                {
                    var bodyString = await response.Content.ReadAsStringAsync();
                    var body = JsonSerializer.Deserialize<T>(bodyString,
                        new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

                    return body;
                }

                await DisplayMessageToUseOnNotSuccess(response);


                return default;
            }
            catch (HttpRequestException)
            {
                _toastService.ShowError(NoInternet, "No Connections");
                throw;
            }
        }

        public async Task DeleteAsync(string url)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(new Uri(_httpClient.BaseAddress + url));
                await DisplayMessageToUseOnNotSuccess(response);
                Console.WriteLine(response.StatusCode);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (HttpRequestException)
            {
                _toastService.ShowError(NoInternet, "No Connections");
                throw;
            }
        }

        public async Task PutVoidAsync(string url, object value)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(new Uri(_httpClient.BaseAddress + url), value);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                await DisplayMessageToUseOnNotSuccess(response);

                throw new NotImplementedException("Error handling not implemented yet!");
            }
            catch (HttpRequestException)
            {
                _toastService.ShowError("No Connections", NoInternet);
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string url, object value)
        {
            var response = await PostAsync(url, value);
            var result = await response.Content.ReadAsStringAsync();
            await DisplayMessageToUseOnNotSuccess(response);

            if (typeof(T) == typeof(string))
            {
                return (T) (object) result;
            }

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task PostVoidAsync(string url, object value)
        {
            var result = await PostAsync(url, value);
        }

        private async Task<HttpResponseMessage> PostAsync(string url, object value)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(new Uri(_httpClient.BaseAddress + url), value);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }

                await DisplayMessageToUseOnNotSuccess(response);
                throw new NotImplementedException("Error handling not implemented yet!");
            }
            catch (HttpRequestException)
            {
                _toastService.ShowError(NoInternet, "No Connections");
                throw;
            }
        }

        private async Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var response = await _httpClient.SendAsync(httpRequestMessage);
                return response;
            }
            catch (HttpRequestException)
            {
                _toastService.ShowError(NoInternet, "No Connections");

            }

            return null;
        }

        private async Task DisplayMessageToUseOnNotSuccess(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _toastService.ShowError("You do now have the rights to perform this action!", "Unauthorized");
                throw new UnauthorizedAccessException();
            }

            Console.WriteLine(response.StatusCode);
            var message = await response.Content.ReadAsStringAsync();

            _toastService.ShowError(message, "Error");
        }
    }
}