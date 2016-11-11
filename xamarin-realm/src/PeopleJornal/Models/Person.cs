using System;
using System.Collections.Generic;
using Realms;

namespace PeopleJornal
{
    public class Person : RealmObject, IModelBase
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<PersonDetail> Details { get; }

        public Person()
        {
            Details = new List<PersonDetail>();
            Id = Guid.NewGuid().ToString();
        }
    }


    public class PersonDetail : RealmObject, IModelBase
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceHandler { get; set; }

        public Person Person { get; set; }

        public PersonDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

