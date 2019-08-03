using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {
        public ObservableCollection<Service> Services { get; set; }
        public Command LoadServicesCommand { get; private set; }

        public ServicesViewModel()
        {
            
            Services = new ObservableCollection<Service>();
            LoadServicesCommand = new Command(ExecuteLoadServicesCommand);
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

        
    }
}