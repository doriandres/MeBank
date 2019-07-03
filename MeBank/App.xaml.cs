using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MeBank.Services;
using MeBank.Services.Abstract;
using MeBank.Services.Concrete;
using MeBank.ViewModels;
using MeBank.Views;


namespace MeBank
{
    public partial class App
    {
        public static int SignedUserId { get; private set; }

        public App()
        {
            InitializeComponent();
            SetUpConfigurations();
        }

        public void SetUpConfigurations()
        {
            RegisterRepositoryServices();
            var config = DependencyService.Get<ConfigRepositoryService>();


            MessagingCenter.Subscribe<SignInViewModel, int>(this, "UserSignedIn", AccountControlHandler);

            MessagingCenter.Subscribe<SignUpViewModel, int>(this, "UserSignedUp", AccountControlHandler);

            MessagingCenter.Subscribe<UserSettingsPage, int>(this, "UserSignedOut", AccountControlHandler);

            var task = Task.Run(() => config.GetAsync("SignedUserId"));
            task.Wait();
            int.TryParse(task.Result, out int id);
            AccountControlHandler(null, id);
        }

        public void RegisterRepositoryServices()
        {
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<DataBaseConnectionManager>();
            DependencyService.Register<ConfigRepositoryService>();
            DependencyService.Register<UserRepositoryService>();
            DependencyService.Register<AccountRepositoryService>();
            DependencyService.Register<PaymentRepositoryService>();
            DependencyService.Register<ServiceRepositoryService>();
            DependencyService.Register<TransferRepositoryService>();
        }

        private void AccountControlHandler(object sender, int userId)
        {
            SignedUserId = userId;

            if (userId == 0)
            {
                MainPage = new AccountAppShell();
            }
            else
            {
                MainPage = new AppShell();
            }
        }

        public static Task Alert(string title, string message, string cancel) => Current.MainPage.DisplayAlert(title, message, cancel);

        public static Task Alert(string title, string message, string accept, string cancel) => Current.MainPage.DisplayAlert(title, message, accept, cancel);

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
