using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Todo.Blazor.Services.Interfaces;

namespace Todo.Blazor.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response =  await _httpClient.GetAsync(_httpClient.BaseAddress + url);
            Console.WriteLine(response.StatusCode + await response.Content.ReadAsStringAsync());
            var bodyString = await response.Content.ReadAsStringAsync();
            var body = JsonSerializer.Deserialize<T>(bodyString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            return body;
        }

        public async Task DeleteAsync(string url)
        {
            await _httpClient.DeleteAsync(new Uri(_httpClient.BaseAddress + url));
        }

        public async Task PutVoidAsync(string url, object value)
        {
            var response = await _httpClient.PutAsJsonAsync(new Uri(_httpClient.BaseAddress+ url), value);
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NotImplementedException("Error handling not implemented yet!");
        }

        public async Task<T> PostAsync<T>(string url, object value)
        {
            var response = await PostAsync(url, value);
            var result = await response.Content.ReadAsStringAsync();

            if (typeof(T) == typeof(string))
            {

                return (T)(object) result;
            }

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task PostVoidAsync(string url, object value)
        {
            await PostAsync(url, value);
        }

        private async Task<HttpResponseMessage> PostAsync(string url, object value)
        {
            var response = await _httpClient.PostAsJsonAsync(new Uri(_httpClient.BaseAddress  + url), value);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }

            throw new NotImplementedException("Error handling not implemented yet!");
        }
    }
}