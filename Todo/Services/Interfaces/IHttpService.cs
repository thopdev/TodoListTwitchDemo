using System.Net.Http;
using System.Threading.Tasks;

namespace Todo.Services.Interfaces
{
    public interface IHttpService
    {
        Task PutVoidAsync(string url, object value);
        Task<T> PostAsync<T>(string url, object value);
        Task PostVoidAsync(string url, object value);
        Task<T> GetAsync<T>(string url);
    }
}