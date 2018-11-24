using System;

namespace FintecDemo.API.Models
{
    public class ExchangeDetailsModel : ExchangeListModel
    {
        /// <summary>
        /// The exchange's country 
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The exchange's city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Specifies when the exchange was modified the last time
        /// </summary>
        public DateTime LastModified { get; set; }
    }
}
