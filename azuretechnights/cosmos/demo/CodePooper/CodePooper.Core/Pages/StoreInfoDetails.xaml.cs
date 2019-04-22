using System;
using CodePooper.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CodePooper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreInfoDetails : ContentPage
    {
        bool isNewItem;

        public StoreInfoDetails(bool isNew)
        {
            InitializeComponent();
            isNewItem = isNew;
        }


        async void OnSaveActivated(object sender, EventArgs e)
        {
            var storeInfo = (StoreInfo)BindingContext;
            await App.StoreInfoManager.SaveStoreInfoAsync(storeInfo, isNewItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteActivated(object sender, EventArgs e)
        {
            var storeInfo = (StoreInfo)BindingContext;
            await App.StoreInfoManager.DeleteStoreInfoAsync(storeInfo);
            await Navigation.PopAsync();
        }

        async void OnCancelActivated(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
