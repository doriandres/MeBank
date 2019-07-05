using MeBank.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeBank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InternetServicePage : ContentPage
    {
        public InternetServicePage()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).NavigationContext = Navigation;
        }
    }
}