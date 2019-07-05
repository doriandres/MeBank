using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MeBank.Models;
using MeBank.Views;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {
        public ObservableCollection<ServiceItem> Services { get; set; }

        public ServicesViewModel()
        {
            var task = Task.Run(() => serviceRepository.FindAllAsync());
            task.Wait();
            var services = task.Result;
            Services = new ObservableCollection<ServiceItem>
            {
                new ServiceItem {Image = "energy_service_icon.png", Service = services.FirstOrDefault(s => s.Description == "Electricidad"), Command = new Command(GoToEnergyServicePage)},
                new ServiceItem {Image = "water_service_icon.png", Service = services.FirstOrDefault(s => s.Description == "Agua"), Command = new Command(GoToWaterServicePage)},
                new ServiceItem {Image = "phone_service_icon.png", Service = services.FirstOrDefault(s => s.Description == "Telefonía"), Command = new Command(GoToPhoneServicePage)},
                new ServiceItem {Image = "internet_service_icon.png", Service = services.FirstOrDefault(s => s.Description == "Internet"), Command =  new Command(GoToInternetServicePage)}
            };
        }

        public async void GoToEnergyServicePage()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new ElectricalServicePage()));
        }

        public async void GoToWaterServicePage()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new WaterServicePage()));
        }

        public async void GoToPhoneServicePage()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new PhoneServicePage()));
        }

        public async void GoToInternetServicePage()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new InternetServicePage()));
        }
    }
}