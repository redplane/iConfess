using Database.Enumerations;
using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.ViewModels.Token
{
    public class FindTokensViewModel
    {
        /// <summary>
        /// Who owns the token.
        /// </summary>
        public int [] OwnerIndexes { get; set; }

        /// <summary>
        /// Types of token.
        /// </summary>
        public TokenType [] Types { get; set; }

        /// <summary>
        /// Token code.
        /// </summary>
        public TextSearch Code { get; set; }

        /// <summary>
        /// When the token was issued.
        /// </summary>
        public UnixDateRange Issued { get; set; }

        /// <summary>
        /// When the token should be expired.
        /// </summary>
        public UnixDateRange Expired { get; set; }

        /// <summary>
        /// Which property should be used for sorting.
        /// </summary>
        public TokenSort Sort { get; set; }

        /// <summary>
        /// Whether records are sorted ascendingly or decendingly.
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Token search pagination.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}