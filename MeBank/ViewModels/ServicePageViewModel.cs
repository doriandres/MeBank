using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class ServicePageViewModel : BaseViewModel
    {

        private string serviceCode;
        private int amountToPay;
        private bool isAmountVisible;
        public ObservableCollection<Account> Accounts { get; set; }
        private Account selectedAccount;

        public ServicePageViewModel()
        {
            Accounts = new ObservableCollection<Account>();
            PayServiceCommand = new Command(ExecutePayServiceCommand);
            FetchPriceCommand = new Command(ExecuteFetchPriceCommand);
            
        }

        public Command PayServiceCommand { get; }
        public Command FetchPriceCommand { get; }


        public string ServiceCode 
        {
            get => serviceCode;
            set => SetProperty(ref serviceCode, value);
        }

        public int AmountToPay
        {
            get => amountToPay;
            set => SetProperty(ref amountToPay, value);
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

        public async void ExecuteFetchPriceCommand()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            if (string.IsNullOrEmpty(ServiceCode))
            {
                await App.Alert("Error", "Por favor ingrese su código de cliente", "Aceptar");
                IsBusy = false;
                return;
            }

            try
            {
                await LoadAccounts();
            }
            catch
            {
                await App.Alert("Error", "Hubo un problema consiguiendo la información del servicio, por favor inténtelo más tarde", "Aceptar");
                IsBusy = false;
                return;
            }
            

            AmountToPay = new Random().Next(10000, 35000);
            IsAmountVisible = true;
            IsBusy = false;
        }

        private async Task LoadAccounts()
        {
            Accounts.Clear();
            var accounts = await AccountApi.GetAccountsAsync(App.SignedUserToken);
            accounts = accounts.Where(a => a.UserId == App.SignedUserId).ToList();
            foreach (var account in accounts)
            {
                Accounts.Add(account);
            }
        }

        public async void ExecutePayServiceCommand()
        {
            IsBusy = true;

            if (SelectedAccount == null)
            {
                IsBusy = false;
                await App.Alert("Error", "Seleccione una cuenta", "Aceptar");
                return;
            }

            var finalAmount = SelectedAccount.Currency == "COL" ? AmountToPay :
                SelectedAccount.Currency == "DOL" ? AmountToPay / 585 : AmountToPay / 650;

            if (SelectedAccount.Balance < finalAmount)
            {
                IsBusy = false;
                await App.Alert("Error", "No tiene suficientes fondos", "Aceptar");
                return;
            }

            var balance = SelectedAccount.Balance;
            SelectedAccount.Balance -= finalAmount;

            var accountModified = await AccountApi.ModifyAccountAsync(SelectedAccount, App.SignedUserToken);

            var paymentAdded  = await PaymentApi.AddPaymentAsync(new Payment
            {
                AccountId = SelectedAccount.Id,
                Date = DateTime.Now,
                Amount = finalAmount,
                ServiceId = App.ServiceId
            }, App.SignedUserToken);

            if (accountModified == null || paymentAdded == null)
            {
                if (paymentAdded == null)
                {
                    SelectedAccount.Balance = balance;
                    await AccountApi.ModifyAccountAsync(SelectedAccount, App.SignedUserToken);
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