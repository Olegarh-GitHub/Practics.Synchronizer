using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.DAL.Models
{
    public class AwardStatusRaw : RawEntity
    {
        public string Name { get; set; }
        
        public AwardStatusRaw
        (
            byte[] id,
            string name, 
            int hash
        ) : base(id, hash)
        {
            Name = name;
        }
    }
}