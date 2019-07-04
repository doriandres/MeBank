using System.ComponentModel;
using MeBank.ViewModels;

namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class DepositPage
    {
        public DepositPage()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).NavigationContext = Navigation;
        }
    }
}