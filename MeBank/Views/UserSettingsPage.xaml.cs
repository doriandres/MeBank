using System.ComponentModel;
using MeBank.ViewModels;
using Xamarin.Forms;

namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class UserSettingsPage : ContentPage
    {
        public UserSettingsPage()
        {
            InitializeComponent();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                listView.SelectedItem = null;
            }
            ((UserSettingsViewModel)BindingContext).UserSignOutCommand.Execute(null);
        }
    }
}