using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MeBank.Services.Abstract;
using MeBank.Services.API;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected IConfigService config => DependencyService.Get<IConfigService>();
        protected IUserService User => DependencyService.Get<IUserService>();
        protected IAccountService Account => DependencyService.Get<IAccountService>();
        protected IPaymentService Payment => DependencyService.Get<IPaymentService>();
        protected ITransferService Transfer => DependencyService.Get<ITransferService>();
        protected IServiceService Service => DependencyService.Get<IServiceService>();

        protected UserApiService UserApi => DependencyService.Get<UserApiService>();
        protected AccountApiService AccountApi => DependencyService.Get<AccountApiService>();
        protected PaymentsApiService PaymentApi => DependencyService.Get<PaymentsApiService>();
        protected TransferenceApiService TransferApi => DependencyService.Get<TransferenceApiService>();
        protected ServiceApiService ServiceApi => DependencyService.Get<ServiceApiService>();

        public INavigation NavigationContext { get; set; }

        private bool isBusy;
        private string title = string.Empty;
        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseViewModel()
        {
            CancelCommand = new Command(ExecuteCancelCommand);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public Command CancelCommand { get; }
        public async void ExecuteCancelCommand()
        {
            await NavigationContext.PopModalAsync();
        }

        protected bool SetProperty<T>(ref T backingStore, T value,  [CallerMemberName]string propertyName = "",  Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
