using System;
using System.Collections.Generic;
using Realms;

namespace PeopleJornal
{
    public class Person : RealmObject
    {
        [ObjectId]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public RealmList<PersonDetail> Details { get; }

        public Person()
        {
            Id = Guid.NewGuid().ToString();
        }
    }


    public class PersonDetail : RealmObject
    {
        [ObjectId]
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceHandler { get; set; }

        public PersonDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

