using System.ComponentModel.DataAnnotations;

namespace FintecDemo.API.Models
{
    /// <summary>
    /// Use an instance of <see cref="ExchangeUpdateModel"/> to modify existing exchanges
    /// </summary>
    public class ExchangeUpdateModel
    {
        /// <summary>
        /// The exchange's city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The exchange's name
        /// </summary>
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        /// <summary>
        /// The exchange's country
        /// </summary>
        [Required]
        [MinLength(3)]
        public string Country { get; set; }
    }
}
