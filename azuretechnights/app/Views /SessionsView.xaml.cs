using System;
using System.Collections.Generic;
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
        }

        protected override async void OnAppearing()
        {
            ViewModel.RefreshSessionCommand.Execute(null);
            base.OnAppearing();
        }

    }
}
