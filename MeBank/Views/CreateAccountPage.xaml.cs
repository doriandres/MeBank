using MeBank.ViewModels;
using Xamarin.Forms.Xaml;

namespace MeBank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage
    {
        public CreateAccountPage()
        {
            InitializeComponent();
            ((CreateAccountViewModel) BindingContext).NavigationContext = Navigation;
        }
    }
}