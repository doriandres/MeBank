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
                },
                new SettingsItem {
                    Name = "Eliminar cuenta",
                    Command = new Command(ExecuteDeleteAccount)
                }
            };
        }

        public async void ExecuteGoToDepositPage()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new DepositPage()));
        }

        public async void ExecuteDeleteAccount()
        {
            if (await App.Alert("Alerta", "¿Seguro que desea eliminar la cuenta?", "Eliminar", "Cancelar"))
            {
                var removed = await AccountApi.RemoveAccountAsync(App.AccountId, App.SignedUserToken);
                if (removed)
                {
                    MessagingCenter.Send(this, "AccountRemoved");
                    await App.Alert("Listo", "La cuenta ha sido eliminada exitosamente", "Aceptar");
                }
                else
                {
                    await App.Alert("Error", "Hubo un error con la cuenta y no se pudo eliminar, inténtelo más tarde.", "Aceptar");
                }
                
                ExecuteCancelCommand();
            }
        }
    }
}