using Xamarin.Forms;
using AzureTechNights.Views;

namespace AzureTechNights
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var content = new SessionsView();
            MainPage = new NavigationPage(content);
        }
    }
}
