using Practics.Synchronizer.Core.Interfaces;

namespace Practics.Synchronizer.Core.Models.Base
{
    public abstract class ImportedEntity : IEntity<int>, IPublishable
    {
        public int Id { get; set; }
        public int Hash { get; set; }
        public string ExtKey { get; set; }
        
        public bool MarkedForCreate { get; set; }
        public bool MarkedForUpdate { get; set; }
        public bool MarkedForDelete { get; set; }
    }
}