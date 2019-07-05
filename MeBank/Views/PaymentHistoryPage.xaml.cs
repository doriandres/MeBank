using System.ComponentModel;
using MeBank.ViewModels;
using Xamarin.Forms;


namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class PaymentHistoryPage 
    {
        public PaymentHistoryPage()
        {
            InitializeComponent();
            ((BaseViewModel) BindingContext).NavigationContext = Navigation;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView) sender;
            if(listView.SelectedItem != null)
            {
                listView.SelectedItem = null;
            }
        }
    }
}