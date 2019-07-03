using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using MeBank.Services.Abstract;

namespace MeBank.Services.Concrete
{
    public class ConfigRepositoryService : EntityRepositoryService<Config>, IConfigRepositoryService
    {
        /// <summary>
        /// Gets the value of a config
        /// </summary>
        /// <param name="key">Configuration key</param>
        /// <returns>Configuration value</returns>
        public async Task<string> GetAsync(string key)
        {
            var data = await FindAllWhereAsync(c => c.Key == key);
            var item = data?.FirstOrDefault();
            return item?.Value;
        }
        
        /// <summary>
        /// Creates or updates a configuration key
        /// </summary>
        /// <param name="key">Key to update or create</param>
        /// <param name="value">Value to update or set</param>
        /// <returns></returns>
        public async Task<int> SetAsync(string key,string value) => (await SaveAsync((await FindAllWhereAsync(c => c.Key == key)).FirstOrDefault() ?? new Config
        {
            Key = key,
            Value = value
        }));
    }
}