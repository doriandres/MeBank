using MeBank.Models.Concrete;
using MeBank.Services.Abstract;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
        private string description;
        private string currency;

        public CreateAccountViewModel()
        {
            CreateAccountCommand = new Command(ExecuteCreateAccountCommand);
        }

        public Command CreateAccountCommand { get; }
        

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value.Trim());
        }

        public string Currency
        {
            get => currency;
            set => SetProperty(ref currency, value);
        }

       

        public async void ExecuteCreateAccountCommand()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            if (string.IsNullOrEmpty(Description))
            {
                await App.Alert("Error", "La descripción es inválida", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Currency))
            {
                await App.Alert("Error", "El tipo de moneda es inválido", "Aceptar");
                return;
            }

            var account = new Account
            {
                Description = Description,
                Currency = Currency,
                Balance = 0,
                Status = "A",
                UserId = App.SignedUserId
            };

            var changes = await accountRepository.SaveAsync(account);

            IsBusy = false;
            if (changes == 0)
            {
                await App.Alert("Error", "No se pudo crear su cuenta, inténtelo máss tarde.", "Aceptar");
                return;
            }

            MessagingCenter.Send(this, "AccountAdded", account);
            // This is just for getting out
            ExecuteCancelCommand();

        }
    }
}