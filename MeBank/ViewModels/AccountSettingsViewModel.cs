using System.Collections.ObjectModel;
using MeBank.Models;
using MeBank.Views;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class AccountSettingsViewModel : BaseViewModel
    {
        public ObservableCollection<SettingsItem> Settings { get; }
        public AccountSettingsViewModel()
        {
            Settings = new ObservableCollection<SettingsItem>
            {
                new SettingsItem {
                    Name = "Depositar dinero", 
                    Command = new Command(ExecuteGoToDepositPage)
                }
            };
        }

        public async void ExecuteGoToDepositPage()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new DepositPage()));
        }
    }
}