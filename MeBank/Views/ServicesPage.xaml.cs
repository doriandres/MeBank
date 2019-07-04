using System.ComponentModel;
using Xamarin.Forms;

namespace MeBank.Views
{
    [DesignTimeVisible(false)]
    public partial class ServicesPage : ContentPage
    {
        public ServicesPage()
        {
            InitializeComponent();
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView) sender;
            listView.SelectedItem = null;
        }
    }
}