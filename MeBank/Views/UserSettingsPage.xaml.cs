using MeBank.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeBank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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