using System;
using Xamarin.Forms;

namespace PeopleJornal
{
    public class PeopleJornalApp : Application
    {
        public PeopleJornalApp()
        {
            ViewModelBase.Initialize();

            MainPage = new NavigationPage(new PeopleJornalPage())
            {
                BarBackgroundColor = Color.FromHex("#E91E63"),
                BarTextColor = Color.White
            };
        }
    }
}
