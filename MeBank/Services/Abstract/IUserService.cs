using System.Threading.Tasks;
using MeBank.Models.Concrete;

namespace MeBank.Services.Abstract
{

    public interface IUserService : IEntityService<User>
    {
        Task<User> Authenticate(string username, string password);
    }
}