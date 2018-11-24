using System;

namespace FintecDemo.API.Entities
{
    public class Stock : IModificationTracker
    {
        private DateTime _created;
        private DateTime _lastModified;
        
        public Guid Id { get; set; }
        public string Isin { get; set; }
        
        public CompanyProfile CompanyProfile { get; set; }
        
        public DateTime Created => _created;
        public DateTime LastModified => _lastModified;
    }
}
