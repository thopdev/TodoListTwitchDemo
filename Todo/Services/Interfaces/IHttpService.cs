using System.Threading.Tasks;

namespace Todo.Blazor.Services.Interfaces
{
    public interface IHttpService
    {
        Task PutVoidAsync(string url, object value);
        Task<T> PostAsync<T>(string url, object value);
        Task PostVoidAsync(string url, object value);
        Task<T> GetAsync<T>(string url);
        Task DeleteAsync(string url);
    }
}