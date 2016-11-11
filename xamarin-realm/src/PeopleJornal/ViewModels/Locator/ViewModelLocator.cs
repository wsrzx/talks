using System;
namespace PeopleJornal
{
    public class ViewModelLocator
    {
        public PeopleJornalViewModel PeopleJornalViewModel { get; set; }

        public AddPeopleViewModel AddPeopleViewModel { get; set; }

        public AddDetailViewModel AddDetailViewModel { get; set; }

        public ViewModelLocator()
        {
            var personService = new PersonService();

            PeopleJornalViewModel = new PeopleJornalViewModel(personService);
            AddPeopleViewModel = new AddPeopleViewModel(personService);
            AddDetailViewModel = new AddDetailViewModel(personService);
        }
    }
}
