using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class DepositViewModel : BaseViewModel
    {
        private string amount;
        private string currency;

        public DepositViewModel()
        {
            SubmitDepositCommand = new Command(ExecuteSubmitDepositCommand);
            var task = Task.Run(() => accountRepository.FindByIdAsync(App.AccountId));
            task.Wait();
            currency = task.Result.Currency;
        }

        public Command SubmitDepositCommand { get; }

        public string Amount { get => amount; set => SetProperty(ref amount, value); }
        public string Currency { get => currency; set => SetProperty(ref currency, value); }

        private decimal decimalAmount => decimal.TryParse(amount, out var val) ? val : 0;

        private async void ExecuteSubmitDepositCommand()
        {
            IsBusy = true;
            if (decimalAmount > 0)
            {
                var account = await accountRepository.FindByIdAsync(App.AccountId);
                account.Balance += decimalAmount;
                var changes = await accountRepository.SaveAsync(account);
                if (changes == 0)
                {
                    IsBusy = false;
                    await App.Alert("Error", "No se pudo realizar el deposito, inténtelo más tarde", "Aceptar");
                }
                else
                {
                    IsBusy = false;
                    MessagingCenter.Send(this, "AccountBalanceChanged");
                    await App.Alert("Listo", "Su deposito se ha realizado existósamente", "Aceptar");
                    ExecuteCancelCommand();
                    ExecuteCancelCommand();
                }
            }
            else
            {
                IsBusy = false;
                await App.Alert("Error", "El monto a depositar es inválido", "Aceptar");
            }
        }
    }
}