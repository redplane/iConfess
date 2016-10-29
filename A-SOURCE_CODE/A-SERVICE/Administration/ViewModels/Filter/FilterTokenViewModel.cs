using Administration.Enumerations;

namespace Administration.ViewModels.Filter
{
    public class FilterTokenViewModel
    {
        /// <summary>
        /// Id of token.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Owner id of token.
        /// </summary>
        public int? Owner { get; set; }

        /// <summary>
        /// Token code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Which mode should be used for code comparision.
        /// </summary>
        public TextComparision CodeComparision { get; set; }

        /// <summary>
        /// Type of token.
        /// </summary>
        public TokenType[] Types { get; set; }

        /// <summary>
        /// Time after which token should be expired.
        /// </summary>
        public double? MinExpire { get; set; }

        /// <summary>
        /// Time before which token should have been expired.
        /// </summary>
        public double? MaxExpire { get; set; }
    }
}