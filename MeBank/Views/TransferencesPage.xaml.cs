using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeBank.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeBank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransferencesPage : ContentPage
    {
        public TransferencesPage()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).NavigationContext = Navigation;

        }
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItem != null)
            {
                listView.SelectedItem = null;
            }
        }
    }
}