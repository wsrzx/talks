using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Realms;

namespace PeopleJornal
{
    public class PersonService : IPersonService
    {
        protected Realm RealmInstance;

        public PersonService()
        {
            RealmInstance = Realm.GetInstance();
        }

        public void Delete(string id)
        {
            using (var trans = RealmInstance.BeginWrite())
            {
                RealmInstance.Remove(GetById(id));
                trans.Commit();
            }
        }

        public List<Person> Find(string searchTerm)
        {
            return RealmInstance
                .All<Person>()
                .Where(p => p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm))
                .ToList();
        }

        public List<Person> Get()
        {
            return RealmInstance.All<Person>().ToList();
        }

        public Person GetById(string id)
        {
            var list = Get();
            return list.FirstOrDefault(p => p.Id == id);
        }

        public bool Save(string personId, string firstName, string lastName, string email)
        {
            try
            {
                RealmInstance.Write(() =>
                {
                    var person = RealmInstance.CreateObject<Person>();
                    person.Id = personId;
                    person.FirstName = firstName;
                    person.LastName = lastName;
                    person.Email = email;
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveDetails(string personId, string serviceName, string serviceHandler)
        {
            var person = GetById(personId);

            try
            {
                RealmInstance.Write(() =>
                {
                    var detail = RealmInstance.CreateObject<PersonDetail>();
                    detail.ServiceName = serviceName;
                    detail.ServiceHandler = serviceHandler;
                    person.Details.Add(detail);

                });

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
