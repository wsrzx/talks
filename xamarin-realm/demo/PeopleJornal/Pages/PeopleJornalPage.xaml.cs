using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeopleJornal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeopleJornalPage : ContentPage
    {
        PeopleViewModel vm;
        PeopleViewModel ViewModel => vm ?? (vm = BindingContext as PeopleViewModel);

        public PeopleJornalPage()
        {
            InitializeComponent();

            ToolbarItems.Add(new ToolbarItem("Add", null, async () => await GoPersonPage()));

            ListView.ItemTapped += async (object sender, ItemTappedEventArgs e) =>
            {
                var person = e.Item as Person;
                await this.Navigation.PushAsync(new AddPersonPage(person.Id));
            };



            BindingContext = vm = new PeopleViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext != null)
                ViewModel.Init();
        }

        protected async Task GoPersonPage()
        {
            await this.Navigation.PushAsync(new AddPersonPage());
        }
    }
}
