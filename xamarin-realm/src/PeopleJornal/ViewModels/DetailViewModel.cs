using System;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace PeopleJornal
{
    [ImplementPropertyChanged]
    public class DetailViewModel : ViewModelBase
    {
        IPersonService _personService;
        public ICommand SaveCommand { get; set; }

        public string PersonId { get; set; }
        public PersonDetail Detail { get; set; }

        public DetailViewModel()
        {
            Title = "Details";
            _personService = DependencyService.Get<IPersonService>();
            SaveCommand = new Command(() => ExecuteSaveCommand());
        }

        public void Init(string id)
        {
            PersonId = id;
            Detail = new PersonDetail();
        }

        void ExecuteSaveCommand()
        {
            _personService.SaveDetails(PersonId, Detail.ServiceName, Detail.ServiceHandler);
        }

    }
}
