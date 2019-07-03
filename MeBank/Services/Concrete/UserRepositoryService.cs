using MeBank.Models.Concrete;
using MeBank.Services.Abstract;

namespace MeBank.Services.Concrete
{
    public class UserRepositoryService : EntityRepositoryService<User>, IUserRepositoryService
    {
        
    }
}