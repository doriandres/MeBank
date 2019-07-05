using MeBank.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeBank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaterServicePage : ContentPage
    {
        public WaterServicePage()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).NavigationContext = Navigation;
        }
    }
}