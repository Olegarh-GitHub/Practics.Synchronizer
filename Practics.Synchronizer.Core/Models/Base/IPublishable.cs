namespace Practics.Synchronizer.Core.Models.Base
{
    public interface IPublishable
    {
        public bool MarkedForCreate { get; set; }
        public bool MarkedForUpdate { get; set; }
        public bool MarkedForDelete { get; set; }
    }
}