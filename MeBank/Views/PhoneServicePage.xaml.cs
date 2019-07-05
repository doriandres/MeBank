using MeBank.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeBank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneServicePage : ContentPage
    {
        public PhoneServicePage()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).NavigationContext = Navigation;
        }
    }
}