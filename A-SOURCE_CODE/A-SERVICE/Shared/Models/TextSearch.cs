using Shared.Enumerations;

namespace Shared.Models
{
    public class TextSearch
    {
        /// <summary>
        ///     Value of text.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     Mode of text filter
        /// </summary>
        public TextComparision Mode { get; set; }
    }
}