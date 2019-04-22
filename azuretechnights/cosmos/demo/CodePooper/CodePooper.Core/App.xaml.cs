using CodePooper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CodePooper
{
    public partial class App : Application
    {
        public static StoreManager StoreManager { get; private set; }

        public App()
        {
            InitializeComponent();
            StoreManager = new StoreManager(new DocumentDBService());
            MainPage = new NavigationPage(new Pages.PoopersPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
