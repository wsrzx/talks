using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CodePooper.Model;

namespace CodePooper.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PoopersPage : ContentPage
    {
        public PoopersPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await App.StoreManager.CreateDatabase(Constants.DatabaseName);
            await App.StoreManager.CreateDocumentCollection(Constants.DatabaseName, Constants.CollectionName);

            var data = await App.StoreManager.GetStoreInfoAsync();

            StoreInfoList.ItemsSource = data;
            StoreInfoList.ItemSelected += OnItemSelected;
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PoopPage(true)
            {
                BindingContext = new Pooper
                {
                    Id = Guid.NewGuid().ToString()
                }
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new PoopPage(false)
                {
                    BindingContext = e.SelectedItem as Pooper
                });
            }
        }
    }
}
