using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace PeopleJornal
{
    [ImplementPropertyChanged]
    public class PeopleViewModel : ViewModelBase
    {
        IPersonService _personService;

        public ObservableCollection<Person> People { get; set; }
        public string SearchTerm { get; set; }

        public PeopleViewModel()
        {
            Title = "People Jornal";
            _personService = DependencyService.Get<IPersonService>();
            Init();
        }

        public void Init()
        {
            var people = new ObservableCollection<Person>(_personService.Get());
            People = people;
        }

    }
}
