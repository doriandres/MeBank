using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using MeBank.Views;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {
        public ObservableCollection<Service> Services { get; set; }
        public Command LoadServicesCommand { get; private set; }
        public Command GoToServicePage { get; private set; }

        public ServicesViewModel()
        {
            
            Services = new ObservableCollection<Service>();
            LoadServicesCommand = new Command(ExecuteLoadServicesCommand);
            GoToServicePage = new Command<int>(ExecuteGoToServicePage);
            LoadServicesCommand.Execute(null);
        }

        private async void ExecuteLoadServicesCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Services.Clear();
                var services = (await ServiceApi.GetServicesAsync());
                foreach (var service in services)
                {
                    Services.Add(service);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteGoToServicePage(int serviceId)
        {
            MessagingCenter.Send(this, "ServiceSelected", serviceId);
            await NavigationContext.PushModalAsync(new NavigationPage(new ServicePage()));
        }
    }
}