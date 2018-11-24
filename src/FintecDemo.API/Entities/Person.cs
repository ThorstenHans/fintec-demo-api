using System;

namespace FintecDemo.API.Entities
{
    public class Person: IModificationTracker
    {
        private DateTime _created;
        private DateTime _lastModified;
        
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
        
        public Guid CompanyId { get; set; }
        public CompanyProfile Company { get; set; }
        
        public DateTime LastModified => _lastModified;
        public DateTime Created => _created;
    }
}
