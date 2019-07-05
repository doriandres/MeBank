using MeBank.Models.Concrete;
using Xamarin.Forms;

namespace MeBank.Models
{
    public class ServiceItem
    {
        public string Image { get; set; }
        public Service Service { get; set; }
        public Command Command { get; set; }
    }
}