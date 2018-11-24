using System;

namespace FintecDemo.API.Entities
{
    public interface IModificationTracker
    {
        DateTime LastModified { get; }
        DateTime Created { get; }
    }

    public class Exchange : IModificationTracker
    {
        private DateTime _lastModified;
        private DateTime _created;
        public Guid Id { get; set; }
        public string Shortcut { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime LastModified => _lastModified;
        public DateTime Created => _created;
    }
}
