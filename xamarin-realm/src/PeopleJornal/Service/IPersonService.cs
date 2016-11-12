using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeopleJornal
{
    public interface IPersonService
    {
        bool Save(string personId, string firstName, string lastName, string email);
        bool SaveDetails(string personId, string serviceName, string serviceHandler);
        List<Person> Get();
        List<Person> Find(string searchTerm);
        Person GetById(string personId);
        void Delete(string personId);
    }
}
