using System;
using Practics.Synchronizer.Core.Extensions;
using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.DAL.Models
{
    public class PersonContactsRaw : RawEntity
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WorkEmail { get; set; }
        
        private readonly byte[] _personId;
        public string PersonGuid => _personId.BytesToGuid();
        
        public PersonContactsRaw
        (
            byte[] id,
            string phoneNumber,
            string email,
            string workEmail,
            byte[] personId,
            int hash
        ) : base(id, hash)
        {
            PhoneNumber = phoneNumber;
            Email = email;
            WorkEmail = workEmail;
            _personId = personId;
        }
    }
}