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
        public DoubleRange Joined { get; set; }

        /// <summary>
        ///     Time when account was lastly modified.
        /// </summary>
        public DoubleRange LastModified { get; set; }
        
        /// <summary>
        /// Sorting property.
        /// </summary>
        public Sorting<AccountsSort> Sorting { get; set; }

        /// <summary>
        ///     Pagination of records filter.
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}