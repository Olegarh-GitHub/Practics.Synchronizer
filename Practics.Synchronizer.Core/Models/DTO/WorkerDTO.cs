namespace Practics.Synchronizer.Core.Models.DTO
{
    public class WorkerDTO
    {
        public string WorkerNumber { get; set; }
        public int DepartmentId { get; set; }
        public int ChiefId { get; set; }
        public int PersonId { get; set; }
        public int AwardStatusId { get; set; }
        
        public bool Fired { get; set; }
        public bool MaternityLeave { get; set; }
    }
}