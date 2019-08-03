using System.Threading.Tasks;

namespace MeBank.Services.API
{
    public interface IApiClientManager
    {
        Task<T> GetFromApiAsync<T>(string endPoint);
        Task<T> PostToApiAsync<T>(string endPoint, object data);
    }
}