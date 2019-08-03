using System.Collections.ObjectModel;
using System.Linq;
using MeBank.Models;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class PaymentHistoryViewModel : BaseViewModel
    {
        public ObservableCollection<PaymentItem> Payments { get; set; }
        public Command LoadPaymentsCommand { get; }
        public PaymentHistoryViewModel()
        {
            Payments = new ObservableCollection<PaymentItem>();
            LoadPaymentsCommand = new Command(LoadPayments);
            MessagingCenter.Subscribe<BaseServiceViewModel>(this, "PaymentCreated", (sender) => LoadPayments());
            LoadPayments();
        }

        private async void LoadPayments()
        {
            IsBusy = true;
            Payments.Clear();
            var accounts = await AccountApi.GetAccountsAsync(App.SignedUserToken);
            accounts = accounts.Where(a => a.UserId == App.SignedUserId).ToList();
            var payments = await PaymentApi.GetPaymentsAsync(App.SignedUserToken);
            payments = payments.Where(p => accounts.Any(a => a.Id == p.AccountId)).ToList();
            var services = await ServiceApi.GetServicesAsync();
            services = services.Where(s => payments.Any(p => p.ServiceId == s.Id)).ToList();

            payments.Reverse();
            foreach (var payment in payments)
            {
                var account = accounts.FirstOrDefault(a => a.Id == payment.AccountId);
                Payments.Add(new PaymentItem
                {
                    Payment = payment,
                    Account = account?.Description,
                    Currency = account?.Currency,
                    Service = services.FirstOrDefault(s => s.Id == payment.ServiceId)?.Description
                });
            }
            IsBusy = false;
        }
    }
}