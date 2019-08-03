using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public abstract class BaseServiceViewModel : BaseViewModel
    {
        private string code;
        private string serviceName;
        private string codeName;
        private bool validateOperator;
        private decimal amount;
        private bool isAmountVisible;
        private string selectedOperator;
        private Account selectedAccount;

        protected BaseServiceViewModel(string serviceName, string codeName, bool validateOperator = false)
        {
            this.serviceName = serviceName;
            this.codeName = codeName;
            this.validateOperator = validateOperator;
            GetRecipeAmountCommand = new Command(ExecuteGetRecipeAmountCommand);
            PayCommand = new Command(ExecutePayCommand);
            var task = Task.Run(() => Account.FindAllWhereAsync(a => a.UserId == App.SignedUserId));
            task.Wait();
            Accounts = task.Result;
        }

        public Command GetRecipeAmountCommand { get; }
        public Command PayCommand { get; }

        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }
        public decimal Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public bool IsAmountVisible
        {
            get => isAmountVisible;
            set => SetProperty(ref isAmountVisible, value);
        }

        public Account SelectedAccount
        {
            get => selectedAccount;
            set => SetProperty(ref selectedAccount, value);
        }

        public string SelectedOperator
        {
            get => selectedOperator;
            set => SetProperty(ref selectedOperator, value);
        }

        public List<Account> Accounts { get; }

        public async void ExecuteGetRecipeAmountCommand()
        {

            if (IsAmountVisible || IsBusy) return;
            IsBusy = true;
            if (string.IsNullOrEmpty(Code) || Code.Length < 8 || !int.TryParse(Code, out var val))
            {
                IsBusy = false;
                if (validateOperator && string.IsNullOrEmpty(selectedOperator))
                {
                    await App.Alert("Error", "Seleccione un operador", "Aceptar");
                }
                else
                {
                    await App.Alert("Error", $"{codeName} inválido, introduzca un código de 8 números", "Aceptar");
                }
                return;
            }
            await Task.Delay(500);
            Amount = new Random().Next(10000, 35000);
            IsAmountVisible = true;
            IsBusy = false;
        }

        public async void ExecutePayCommand()
        {
            IsBusy = true;

            if (selectedAccount == null)
            {
                IsBusy = false;
                await App.Alert("Error", "Seleccione una cuenta", "Aceptar");
                return;
            }

            var finalAmount = selectedAccount.Currency == "COL" ? amount :
                selectedAccount.Currency == "DOL" ? amount / 585 : amount / 650;

            if (selectedAccount.Balance < finalAmount)
            {
                IsBusy = false;
                await App.Alert("Error", "No tiene suficientes fondos", "Aceptar");
                return;
            }

            var balance = selectedAccount.Balance;
            selectedAccount.Balance -= finalAmount;
            var changes = await Account.SaveAsync(selectedAccount);
            var service = (await Service.FindAllWhereAsync(s => s.Description == serviceName)).FirstOrDefault();
            var added = 0;

            if (service != null)
            {
                added = await Payment.SaveAsync(new Payment
                {
                    AccountId = selectedAccount.Id,
                    Date = DateTime.Now,
                    Amount = finalAmount,
                    ServiceId = service.Id
                });
            }

            if (changes == 0 || service == null || added == 0)
            {
                if (service == null)
                {
                    selectedAccount.Balance = balance;
                    await Account.SaveAsync(selectedAccount);
                }

                IsBusy = false;
                await App.Alert("Error", "No se pudo realizar el pago, por favor inténtelo más tarde", "Aceptar");

            }
            else
            {
                IsBusy = false;
                MessagingCenter.Send(this, "BalanceChanged");
                MessagingCenter.Send(this, "PaymentCreated");
                await App.Alert("Listo", "Su pago se ha realizado exitosamente", "Aceptar");
                ExecuteCancelCommand();
            }

        }
    }
}