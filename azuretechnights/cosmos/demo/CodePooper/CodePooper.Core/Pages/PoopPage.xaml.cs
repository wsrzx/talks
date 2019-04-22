using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePooper.Model;

namespace CodePooper.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoopPage : ContentPage
    {
        bool isNewItem;

        public PoopPage(bool isNew)
        {
            InitializeComponent();
            isNewItem = isNew;
        }

        async void OnSaveActivated(object sender, EventArgs e)
        {
            var model = (Pooper)BindingContext;
            await App.StoreManager.SaveStoreInfoAsync(model, isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var storeInfo = (Pooper)BindingContext;
            await App.StoreManager.DeleteStoreInfoAsync(storeInfo);
            await Navigation.PopAsync();
        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
