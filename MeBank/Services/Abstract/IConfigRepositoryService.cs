using System.Threading.Tasks;
using MeBank.Models.Concrete;

namespace MeBank.Services.Abstract
{
    public interface IConfigRepositoryService : IEntityRepositoryService<Config>
    {
        Task<string> GetAsync(string key);
        Task<int> SetAsync(string key, string value);
    }
}