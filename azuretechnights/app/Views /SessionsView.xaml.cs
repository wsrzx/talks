using System;
using System.Collections.Generic;
using AzureTechNights.Models;
using AzureTechNights.ViewModels;
using Xamarin.Forms;

namespace AzureTechNights.Views
{
    public partial class SessionsView : ContentPage
    {
        SessionsViewModel ViewModel;

        public SessionsView()
        {
            InitializeComponent();

            ViewModel = new SessionsViewModel();
            BindingContext = ViewModel;

            sessionsListView.ItemSelected += SessionsListViewItemSelected;
        }

        protected override async void OnAppearing()
        {
            ViewModel.RefreshSessionCommand.Execute(null);
            base.OnAppearing();
        }

        private async void SessionsListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var session = e.SelectedItem as Session;

            if (session == null)
                return;

            await Navigation.PushAsync(new SessionBrowserView(session));

            sessionsListView.SelectedItem = null;
        }

    }
}
