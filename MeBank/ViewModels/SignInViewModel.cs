using System.Linq;
using MeBank.Services.Abstract;
using MeBank.Services.Concrete;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private string username;
        private string password;

        public SignInViewModel()
        {
            SignInCommand = new Command(SubmitSignIn);
        }

        public Command SignInCommand { get; }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private async void SubmitSignIn()
        {
            IsBusy = true;
            var results = await userRepository.FindAllWhereAsync(u => u.Username == username && u.Password == password);
            var user = results.FirstOrDefault();
            if (user == null)
            {
                IsBusy = false;
                await App.Alert("Error", "Credenciales inválidas", "Aceptar");
            }
            else
            {
                await config.SetAsync("SignedUserId", user.Id.ToString());
                IsBusy = false;
                MessagingCenter.Send(this, "UserSignedIn", user.Id);
            }
        }
    }
}