using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MeBank.Models.Concrete;
using MeBank.Views;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class AccountsViewModel : BaseViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public Command LoadAccountsCommand { get; set; }
        public Command GoToAddAccountCommand { get; set; }
        public Command GoToAccountSettingsCommand { get; set; }

        public AccountsViewModel()
        {
            Accounts = new ObservableCollection<Account>();
            LoadAccountsCommand = new Command(ExecuteLoadAccountsCommand);
            GoToAddAccountCommand = new Command(ExecuteGoToAddAccountCommand);
            GoToAccountSettingsCommand = new Command<int>(ExecuteGoToAccountSettingsCommand);

            LoadAccountsCommand.Execute(null);

            MessagingCenter.Subscribe<CreateAccountViewModel, Account>(this, "AccountAdded", (sender, account) => ExecuteLoadAccountsCommand());
            MessagingCenter.Subscribe<DepositViewModel>(this, "AccountBalanceChanged", sender => ExecuteLoadAccountsCommand());
            MessagingCenter.Subscribe<AccountSettingsViewModel>(this, "AccountRemoved", (sender) => ExecuteLoadAccountsCommand());
            MessagingCenter.Subscribe<ServicePageViewModel>(this, "BalanceChanged", (sender) => ExecuteLoadAccountsCommand());
            MessagingCenter.Subscribe<NewTransferViewModel>(this, "TransferDone", (sender) => ExecuteLoadAccountsCommand());
        }

        private async void ExecuteLoadAccountsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Accounts.Clear();
                var accounts = await AccountApi.GetAccountsAsync(App.SignedUserToken);
                accounts = accounts.Where(a => a.UserId == App.SignedUserId).ToList();
                foreach (var account in accounts)
                {
                    Accounts.Add(account);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteGoToAddAccountCommand()
        {
            await NavigationContext.PushModalAsync(new NavigationPage(new CreateAccountPage()));
        }

        private async void ExecuteGoToAccountSettingsCommand(int accountId)
        {
            MessagingCenter.Send(this, "AccountSelected", accountId);
            await NavigationContext.PushModalAsync(new NavigationPage(new AccountSettingsPage()));
        }
    }
}