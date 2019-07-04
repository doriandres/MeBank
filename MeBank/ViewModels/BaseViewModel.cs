using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MeBank.Services.Abstract;
using Xamarin.Forms;

namespace MeBank.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected IConfigRepositoryService config => DependencyService.Get<IConfigRepositoryService>();
        protected IUserRepositoryService userRepository => DependencyService.Get<IUserRepositoryService>();
        protected IAccountRepositoryService accountRepository => DependencyService.Get<IAccountRepositoryService>();
        protected IPaymentRepositoryService paymentRepository => DependencyService.Get<IPaymentRepositoryService>();
        protected ITransferRepositoryService transferRepository => DependencyService.Get<ITransferRepositoryService>();
        protected IServiceRepositoryService serviceRepository => DependencyService.Get<IServiceRepositoryService>();

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
