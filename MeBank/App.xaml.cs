using System.Linq;
using System.Threading.Tasks;
using MeBank.Models.Concrete;
using Xamarin.Forms;
using MeBank.Services;
using MeBank.Services.Abstract;
using MeBank.Services.Concrete;
using MeBank.ViewModels;


namespace MeBank
{
    public partial class App
    {
        public static int SignedUserId { get; private set; }
        public static int AccountId { get; private set; }

        public App()
        {
            InitializeComponent();
            SetUpConfigurations();
            CreateDefaultData();
        }

        private void SetUpConfigurations()
        {
            RegisterRepositoryServices();
            SubscribeToAppEvents();
            var config = DependencyService.Get<ConfigRepositoryService>();
            var task = Task.Run(() => config.GetAsync("SignedUserId"));
            task.Wait();
            int.TryParse(task.Result, out int id);
            AccountControlHandler(null, id);
        }

        private void RegisterRepositoryServices()
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

        private void SubscribeToAppEvents()
        {
            MessagingCenter.Subscribe<SignInViewModel, int>(this, "UserSignedIn", AccountControlHandler);
            MessagingCenter.Subscribe<SignUpViewModel, int>(this, "UserSignedUp", AccountControlHandler);
            MessagingCenter.Subscribe<UserSettingsViewModel, int>(this, "UserSignedOut", AccountControlHandler);
            MessagingCenter.Subscribe<AccountsViewModel, int>(this, "AccountSelected", (sender, accountId) =>
            {
                AccountId = accountId;
            });
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

        private async void CreateDefaultData()
        {
            var serviceRepository = DependencyService.Get<IServiceRepositoryService>();
            var electricalService = new Service
            {
                Description = "Electricidad",
                Status = "A"
            };
            var waterService = new Service
            {
                Description = "Agua",
                Status = "A"
            };
            var phoneService = new Service
            {
                Description = "Telefonía",
                Status = "A"
            };
            var internetService = new Service
            {
                Description = "Internet",
                Status = "A"
            };
            var existingServices = await serviceRepository.FindAllAsync();
            if (existingServices.All(s => s.Description != electricalService.Description))
            {
                await serviceRepository.SaveAsync(electricalService);
            }
            if (existingServices.All(s => s.Description != waterService.Description))
            {
                await serviceRepository.SaveAsync(waterService);
            }
            if (existingServices.All(s => s.Description != phoneService.Description))
            {
                await serviceRepository.SaveAsync(phoneService);
            }
            if (existingServices.All(s => s.Description != internetService.Description))
            {
                await serviceRepository.SaveAsync(internetService);
            }
        }

        public static Task Alert(string title, string message, string cancel) => Current.MainPage.DisplayAlert(title, message, cancel);

        public static Task<bool> Alert(string title, string message, string accept, string cancel) => Current.MainPage.DisplayAlert(title, message, accept, cancel);

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
