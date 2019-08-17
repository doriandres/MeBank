using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class NewTransferViewModel : BaseViewModel
    {
        private Account fromAccountSelected;
        private Account toAccountSelected;
        private string amount;
        private string description;

        public Command PerformTransferCommand { get; }
        public ObservableCollection<Account> Accounts { get; set; }

        public Account FromAccountSelected
        {
            get => fromAccountSelected;
            set => SetProperty(ref fromAccountSelected, value);
        }

        public Account ToAccountSelected
        {
            get => toAccountSelected;
            set => SetProperty(ref toAccountSelected, value);
        }

        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public NewTransferViewModel()
        {
            PerformTransferCommand = new Command(ExecutePerformTransferCommand);
            Accounts = new ObservableCollection<Account>();
            Accounts.Clear();
            var task = Task.Run(() => AccountApi.GetAccountsAsync(App.SignedUserToken));
            task.Wait();
            var accounts = task.Result;
            accounts = accounts.Where(a => a.UserId == App.SignedUserId).ToList();
            foreach (var account in accounts)
            {
                Accounts.Add(account);
            }
        }

        private decimal decimalAmount => decimal.TryParse(amount, out var val) ? val : 0;

        private async void ExecutePerformTransferCommand()
        {
            IsBusy = true;
            if (decimalAmount <= 0)
            {
                IsBusy = false;
                await App.Alert("Error", "El monto a depositar es inválido", "Aceptar");
                return;
            }

            if (FromAccountSelected == ToAccountSelected )
            {
                IsBusy = false;
                await App.Alert("Error", "La cuenta de origen y la cuenta de destino deben ser diferentes", "Aceptar");
                return;
            }

            if (FromAccountSelected.Balance < decimalAmount)
            {
                IsBusy = false;
                await App.Alert("Error", "No tiene suficientes fondos en la cuenta para realizar la transferencia", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Description))
            {
                IsBusy = false;
                await App.Alert("Error", "Por favor ingrese una descripción", "Aceptar");
                return;
            }

            var fromBalance = FromAccountSelected.Balance;
            var toBalance = ToAccountSelected.Balance;

            FromAccountSelected.Balance -= decimalAmount;
            ToAccountSelected.Balance += decimalAmount;
                
            var fromAccountModified = await AccountApi.ModifyAccountAsync(FromAccountSelected, App.SignedUserToken);
            var toAccountModified = await AccountApi.ModifyAccountAsync(ToAccountSelected, App.SignedUserToken);
            var transfer = await TransferApi.AddTransferenceAsync(new Transfer
            {
                Amount = decimalAmount,
                Date = DateTime.Now,
                Description = Description,
                DestinyAccountId = ToAccountSelected.Id,
                OriginAccountId = FromAccountSelected.Id,
                Status = "A"
            }, App.SignedUserToken);
            

            if (fromAccountModified == null || toAccountModified == null || transfer == null)
            {
                FromAccountSelected.Balance = fromBalance;
                await AccountApi.ModifyAccountAsync(FromAccountSelected, App.SignedUserToken);
                ToAccountSelected.Balance = toBalance;
                await AccountApi.ModifyAccountAsync(ToAccountSelected, App.SignedUserToken);

                await App.Alert("Error", "No se pudo realizar la transferencia, inténtelo más tarde", "Aceptar");
                IsBusy = false;
            }
            else
            {
                IsBusy = false;
                MessagingCenter.Send(this, "TransferDone");
                await App.Alert("Listo", "La transferencia se ha realizado existósamente", "Aceptar");
                ExecuteCancelCommand();
            }
        }
    }
}