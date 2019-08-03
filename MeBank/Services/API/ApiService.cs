using Xamarin.Forms;

namespace MeBank.Services.API
{
    public abstract class ApiService
    {
        protected IApiClientManager api;
        protected ApiService()
        {
            api = DependencyService.Get<ApiClientManager>();
        }
    }
}