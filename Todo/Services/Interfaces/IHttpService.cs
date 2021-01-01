using System.Net.Http;
using System.Threading.Tasks;

namespace Todo.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> PostAsync<T>(string url, object value);
        Task PostVoidAsync(string url, object value);
    }
}