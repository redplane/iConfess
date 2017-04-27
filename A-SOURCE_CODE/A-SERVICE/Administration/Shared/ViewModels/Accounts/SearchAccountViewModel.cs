using Database.Enumerations;
using Shared.Enumerations.Order;
using Shared.Models;

namespace Shared.ViewModels.Accounts
{
    public class SearchAccountViewModel
    {
        /// <summary>
        ///     Page of account.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        ///     Email address.
        /// </summary>
        public TextSearch Email { get; set; }

        /// <summary>
        ///     Nickname of account owner.
        /// </summary>
        public TextSearch Nickname { get; set; }

        /// <summary>
        ///     Account status.
        /// </summary>
        public Statuses[] Statuses { get; set; }

        /// <summary>
        ///     Time when account joined.
        /// </summary>
        public UnixDateRange Joined { get; set; }

        /// <summary>
        ///     Time when account was lastly modified.
        /// </summary>
        public UnixDateRange LastModified { get; set; }

        /// <summary>
        ///     Which property should be used for sorting.
        /// </summary>
        public AccountsSort Sort { get; set; }

        /// <summary>
        ///     Whether accounts should be sorted asc or desc.
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        ///     Pagination of records filter.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}