using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace PeopleJornal
{
    [ImplementPropertyChanged]
    public class PersonViewModel : ViewModelBase
    {
        IPersonService _personService;
        public Person Person { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public PersonViewModel()
        {
            Title = "Person";
            _personService = DependencyService.Get<IPersonService>();
            SaveCommand = new Command(() => ExecuteSaveCommand());
            DeleteCommand = new Command(() => ExecuteDeleteCommand());
        }

        public void Init(string id)
        {
            Person = (!string.IsNullOrEmpty(id)) ? _personService.GetById(id) : new Person();
        }

        protected void ExecuteSaveCommand()
        {
            _personService.Save(Person.Id, Person.FirstName, Person.LastName);
        }

        protected void ExecuteDeleteCommand()
        {
            _personService.Delete(Person.Id);
        }


    }
}
