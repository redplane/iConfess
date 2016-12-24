using System.Linq;
using iConfess.Database.Models.Tables;

namespace Shared.ViewModels.Accounts
{
    public class ResponseAccountsViewModel
    {
        /// <summary>
        /// List of accounts which has been filtered.
        /// </summary>
        public IQueryable<Account> Accounts { get; set; }

        /// <summary>
        /// Total of accounts match with conditions.
        /// </summary>
        public int Total { get; set; }
    }
}