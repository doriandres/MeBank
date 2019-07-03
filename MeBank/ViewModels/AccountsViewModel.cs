using System.Collections.ObjectModel;
using MeBank.Models.Concrete;
using MeBank.Services.Abstract;
using MeBank.Views;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class AccountsViewModel : BaseViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public Command LoadAccountsCommand { get; set; }
        public Command GoToAddAccountCommand { get; set; }

        public AccountsViewModel()
        {
            Accounts = new ObservableCollection<Account>();
            LoadAccountsCommand = new Command(ExecuteLoadAccountsCommand);
            GoToAddAccountCommand = new Command(ExecuteGoToAddAccountCommand);
            LoadAccountsCommand.Execute(null);

            MessagingCenter.Subscribe<CreateAccountViewModel, Account>(this,"AccountAdded", (sender, account)=>{
                Accounts.Add(account);
            });
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
                var accounts = await accountRepository.FindAllWhereAsync(a => a.UserId == App.SignedUserId);
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
    }
}