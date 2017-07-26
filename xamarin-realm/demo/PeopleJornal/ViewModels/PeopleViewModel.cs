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

        private string _searchTerm;
        public string SearchTerm
        {
            get
            {
                return _searchTerm;
            }
            set
            {
                _searchTerm = value;
                People = new ObservableCollection<Person>(_personService.Find(_searchTerm));
            }
        }

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
