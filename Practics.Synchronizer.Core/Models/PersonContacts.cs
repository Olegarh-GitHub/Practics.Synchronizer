using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Models
{
    public class PersonContacts : ImportedEntity
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WorkEmail { get; set; }
        
        public Person Person { get; set; }
        public int PersonId { get; set; }
    }
}