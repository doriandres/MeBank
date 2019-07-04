using System.Collections.ObjectModel;
using MeBank.Models;
using MeBank.Models.Concrete;

namespace MeBank.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {
        public ObservableCollection<ServiceItem> Services { get; set; }

        public ServicesViewModel()
        {
            Services = new ObservableCollection<ServiceItem>
            {
                new ServiceItem {Image = "energy_service_icon.png", Name = "Electricidad"},
                new ServiceItem {Image = "water_service_icon.png", Name = "Agua"},
                new ServiceItem {Image = "phone_service_icon.png", Name = "Telefonía"},
                new ServiceItem {Image = "internet_service_icon.png", Name = "Internet"}
            };
        }
    }
}