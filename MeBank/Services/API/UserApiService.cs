using System.Threading.Tasks;
using MeBank.Models.Concrete;

namespace MeBank.Services.API
{
    public class UserApiService : ApiService
    {
        public async Task<User> Authenticate(string username, string password)
        {
            return await api.PostToApiAsync<User>(ApiEndPoints.Authentication, new 
            {
                username,
                password
            });
        }
    }
}