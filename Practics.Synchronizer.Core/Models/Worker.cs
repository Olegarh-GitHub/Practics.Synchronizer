using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Models
{
    public class Worker : ImportedEntity
    {
        public string WorkerNumber { get; set; }
        
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        
        public Worker Chief { get; set; }
        public int? ChiefId { get; set; }
        
        public Person Person { get; set; }
        public int PersonId { get; set; }
        
        public AwardStatus AwardStatus { get; set; }
        public int? AwardStatusId { get; set; }
        
        public bool Fired { get; set; }
        public bool MaternityLeave { get; set; }
    }
}