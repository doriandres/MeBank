using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using MeBank.Services.Abstract;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private string username;
        private string password;
        private string citizenId;
        private string name;
        private string email;
        private DateTime birthdate;

        public SignUpViewModel()
        {
            SignUpCommand = new Command(SubmitSignUp);
        }

        public Command SignUpCommand { get; }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value.Trim());
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value.Trim());
        }

        public string CitizenId
        {
            get => citizenId;
            set => SetProperty(ref citizenId, value.Trim());
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value.Trim());
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value.Trim());
        }

        public DateTime Birthdate
        {
            get => birthdate;
            set => SetProperty(ref birthdate, value);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private async Task<string> Validate()
        {
            if (string.IsNullOrEmpty(Username))
            {
                return "Ingrese un nombre de usuario";
            }

            if (string.IsNullOrEmpty(Password))
            {
                return "Ingrese una contraseña";
            }

            if (string.IsNullOrEmpty(CitizenId) || !int.TryParse(CitizenId, out var id))
            {
                return "Ingrese un número de cédula válido";
            }

            if (string.IsNullOrEmpty(Name))
            {
                return "Ingrese un nombre válido";
            }

            if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
            {
                return "Ingrese un correo electrónico válido";
            }

            if (!string.IsNullOrEmpty(Username))
            {
                var users = await User.FindAllWhereAsync(u => u.Username == Username);
                if (users.Count != 0)
                {
                    return "El nombre de usuario ya existe";
                }
            }

            if (!string.IsNullOrEmpty(CitizenId))
            {
                var users = await User.FindAllWhereAsync(u => u.CitizenId == CitizenId);
                if (users.Count != 0)
                {
                    return "El número de cédula ya existe";
                }
            }

            if (!string.IsNullOrEmpty(Email))
            {
                var users = await User.FindAllWhereAsync(u => u.Email == Email);
                if (users.Count != 0)
                {
                    return "El correo electrónico ya existe";
                }
            }

            return null;
        }

        private async void SubmitSignUp()
        {
            IsBusy = true;
            var validationError = await Validate();
            if (string.IsNullOrEmpty(validationError))
            {
                var user = new User
                {
                    Username = Username,
                    Password = Password,
                    CitizenId = CitizenId,
                    Name = Name,
                    Email = Email,
                    Birthday = Birthdate,
                    Status = "A"
                };
                var changes = await User.SaveAsync(user);

                

                if (changes == 0)
                {
                    IsBusy = false;
                    await App.Alert("Error", "No se pudo guardar el usuario, inténtelo más tarde", "Aceptar");
                    return;
                }

                await config.SetAsync("SignedUserId", user.Id.ToString());
                IsBusy = false;
                MessagingCenter.Send(this, "UserSignedUp", user.Id);
            }
            else
            {
                IsBusy = false;
                await App.Alert("Error", validationError, "Aceptar");
            }

        }
    }
}