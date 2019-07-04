using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class UserSettingsViewModel : BaseViewModel
    {
        public Command UserSignOutCommand { get; }

        public UserSettingsViewModel()
        {
            UserSignOutCommand = new Command(ExecuteUserSignOut);
        }

        public async void ExecuteUserSignOut()
        {
            var changes = await config.SetAsync("SignedUserId", "");
            if (changes == 0)
            {
                await App.Alert("Error", "No se pudo cerrar sesión, inténtelo más tarde", "Aceptar");
            }
            else
            {
                MessagingCenter.Send(this, "UserSignedOut", 0);
            }
        }
    }
}