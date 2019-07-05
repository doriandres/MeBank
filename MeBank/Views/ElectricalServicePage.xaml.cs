using MeBank.ViewModels;
using Xamarin.Forms.Xaml;

namespace MeBank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElectricalServicePage 
    {
        public ElectricalServicePage()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).NavigationContext = Navigation;
        }
    }
}