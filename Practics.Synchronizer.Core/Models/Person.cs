using System;
using System.Collections.Generic;
using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Models
{
    public class Person : ImportedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        
        public PersonContacts Contacts { get; set; }
        public ICollection<Worker> Workers { get; set; }
    }
}