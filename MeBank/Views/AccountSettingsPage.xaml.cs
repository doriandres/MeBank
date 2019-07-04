using System.ComponentModel;
using MeBank.Models;
using MeBank.ViewModels;
using Xamarin.Forms;

namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class AccountSettingsPage
    {
        public AccountSettingsPage()
        {
            InitializeComponent();
            ((BaseViewModel) BindingContext).NavigationContext = Navigation;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(sender is ListView listView)) return;
            var settingsItem = listView?.SelectedItem as SettingsItem;
            settingsItem?.Command.Execute(null);
            listView.SelectedItem = null;
        }
    }
}