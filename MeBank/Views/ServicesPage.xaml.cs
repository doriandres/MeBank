using System.ComponentModel;
using MeBank.Models;
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

            var serviceItem = (ServiceItem)listView.SelectedItem;
            serviceItem.Command.Execute(null);
            listView.SelectedItem = null;
        }
    }
}