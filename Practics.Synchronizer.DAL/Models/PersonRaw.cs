using System;
using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.DAL.Models
{
    public class PersonRaw : RawEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }

        public PersonRaw
        (
            byte[] id,
            string firstName,
            string lastName,
            string secondName,
            DateTime birthday,
            string gender,
            int hash
        ) : base(id, hash)
        {
            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
            Birthday = birthday;
            Gender = gender;
        }
    }
}