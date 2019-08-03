using System.Threading.Tasks;
using MeBank.Models.Concrete;

namespace MeBank.Services.Abstract
{
    public interface IConfigService : IEntityService<Config>
    {
        Task<string> GetAsync(string key);
        Task<int> SetAsync(string key, string value);
    }
}