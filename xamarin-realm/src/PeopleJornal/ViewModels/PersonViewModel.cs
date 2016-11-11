using System;
using System.Collections.Generic;
using System.Linq;
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

        public Person Model { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PersonDetail> Details { get; set; }
        public bool Edit { get; set; }

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
            Edit = (id != null);
            Model = (id != null) ? _personService.GetById(id) : new Person();

            FirstName = Model.FirstName;
            LastName = Model.LastName;

            if (Model.Details != null)
                Details = Model.Details.ToList();
        }

        protected void ExecuteSaveCommand()
        {
            _personService.Save(Model.Id, FirstName, LastName);
        }

        protected void ExecuteDeleteCommand()
        {
            _personService.Delete(Model.Id);
        }

        public void Refresh()
        {
            if (Model.Details != null)
                Details = Model.Details.ToList();
        }
    }
}
