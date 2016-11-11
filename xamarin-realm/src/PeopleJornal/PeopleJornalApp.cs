using System;
using Xamarin.Forms;

namespace PeopleJornal
{
    public class PeopleJornalApp : Application
    {
        public PeopleJornalApp()
        {
            ViewModelBase.Init();
            MainPage = new NavigationPage(new PeopleJornalPage());
        }
    }
}
