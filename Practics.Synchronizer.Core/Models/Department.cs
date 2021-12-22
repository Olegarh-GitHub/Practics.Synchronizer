using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Models
{
    public class Department : ImportedEntity
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
    }
}