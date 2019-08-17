using System.Collections.ObjectModel;
using System.Linq;
using MeBank.Models;
using MeBank.Views;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class TransferencesViewModel : BaseViewModel
    {
        public ObservableCollection<TransferItem> Transferences { get; set; }
        public Command LoadTransferencesCommand { get; }
        public Command GoToNewTransactionCommand { get; }
        
        public TransferencesViewModel()
        {
            Transferences = new ObservableCollection<TransferItem>();
            LoadTransferencesCommand = new Command(LoadTransferences);
            GoToNewTransactionCommand = new Command(ExecuteGoToNewTransactionCommand);
            MessagingCenter.Subscribe<NewTransferViewModel>(this, "TransferDone", (sender) => LoadTransferences());
            LoadTransferences();
        }

        private async void LoadTransferences()
        {
            IsBusy = true;
            Transferences.Clear();
            
            var accounts = await AccountApi.GetAccountsAsync(App.SignedUserToken);
            accounts = accounts.Where(a => a.UserId == App.SignedUserId).ToList();
            var transfers = await TransferApi.GetTransferencesAsync(App.SignedUserToken);
            transfers = transfers.Where(t =>accounts.Any(a => a.Id == t.OriginAccountId || a.Id == t.DestinyAccountId)).ToList();
            transfers.Reverse();

            foreach (var transfer in transfers)
            {
                var fromAccount = accounts.FirstOrDefault(a => a.Id == transfer.OriginAccountId);
                var ToAccount = accounts.FirstOrDefault(a => a.Id == transfer.DestinyAccountId);
                Transferences.Add(new TransferItem
                {
                    Transfer = transfer,
                    To = ToAccount?.Description,
                    From = fromAccount?.Description,
                    Currency = fromAccount?.Currency,
                    Amount = transfer.Amount,
                    Date = transfer.Date,
                    Description = transfer.Description
                });
            }
            IsBusy = false;
        }

        private async void ExecuteGoToNewTransactionCommand()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new NewTransferPage()));
        }
    }
}