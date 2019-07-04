using System.ComponentModel;
using MeBank.Models.Concrete;
using MeBank.ViewModels;
using Xamarin.Forms;

namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class AccountsPage
    {
        public AccountsPage()
        {
            InitializeComponent();
            ((AccountsViewModel) BindingContext).NavigationContext = Navigation;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem != null)
            {
                var account = (Account) listView.SelectedItem;
                listView.SelectedItem = null;
                if (account !=  null)
                {
                    ((AccountsViewModel)BindingContext).GoToAccountSettingsCommand.Execute(account.Id);
                }
                
            }
        }
    }
}