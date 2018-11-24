namespace FintecDemo.API.Models
{
    /// <summary>
    /// ExchangeListModel is optimized for displaying lists of exchanges
    /// </summary>
    public class ExchangeListModel
    {
        /// <summary>
        /// Exchange's Identifier 
        /// </summary>
        public string Shortcut { get; set; }

        /// <summary>
        /// Exchange's Name
        /// </summary>
        public string Name { get; set; }
    }
}
