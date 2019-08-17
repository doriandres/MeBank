using System.ComponentModel;
using MeBank.Models.Concrete;
using MeBank.ViewModels;
using Xamarin.Forms;

namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class ServicesPage 
    {
        public ServicesPage()
        {
            InitializeComponent();
            ((BaseViewModel) BindingContext).NavigationContext = Navigation;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            var listView = (ListView) sender;

            if (listView.SelectedItem == null) return;

            var serviceItem = (Service)listView.SelectedItem;
            listView.SelectedItem = null;

            if (serviceItem != null)
            {
                ((ServicesViewModel)BindingContext).GoToServicePage.Execute(serviceItem.Id);
            }
        }
    }
}