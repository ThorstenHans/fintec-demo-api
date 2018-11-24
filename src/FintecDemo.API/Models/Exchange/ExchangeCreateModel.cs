using System.ComponentModel.DataAnnotations;

namespace FintecDemo.API.Models
{
    /// <summary>
    /// Use an instance of `ExchangeCreateModel` to create a new exchange
    /// </summary>
    public class ExchangeCreateModel
    {
        /// <summary>
        /// Unique shortcut of the exchange 
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Shortcut { get; set; }

        /// <summary>
        /// Name of the exchange
        /// </summary>
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        /// <summary>
        /// City where the exchange is located
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Country where the exchange is located
        /// </summary>
        [Required]
        [MinLength(3)]
        public string Country { get; set; }
    }
}
