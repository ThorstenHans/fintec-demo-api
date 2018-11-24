using System;

namespace FintecDemo.API.Entities
{
    public class CompanyProfile : IModificationTracker
    {
        private DateTime _created;
        private DateTime _lastModified;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }

        public Person Ceo { get; set; }

        public Guid StockId { get; set; }
        public Stock Stock { get; set; }

        public DateTime LastModified => _lastModified;
        public DateTime Created => _created;
    }
}
