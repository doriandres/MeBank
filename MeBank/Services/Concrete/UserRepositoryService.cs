using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using MeBank.Services.Abstract;

namespace MeBank.Services.Concrete
{
    public class UserRepositoryService : EntityRepositoryService<User>, IUserService
    {
        public async Task<User> Authenticate(string username, string password)
        {
            var results = await FindAllWhereAsync(u => u.Username == username && u.Password == password);
            return results.FirstOrDefault();
        }
    }
}