using System.Threading.Tasks;
using Administration.Models.Tables;
using Administration.ViewModels.Filter;

namespace Administration.Interfaces
{
    public interface IRepositoryAccount
    {
        /// <summary>
        /// Find account by using specific conditions.
        /// </summary>
        /// <param name="filterAccountViewModel">Conditions which are used for filtering.</param>
        /// <returns></returns>
        Task<Account> FindAccountAsync(FilterAccountViewModel filterAccountViewModel);

        /// <summary>
        /// Create an account and save it into database asynchronously.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<Account> InitializeAccountAsync(Account account);

        /// <summary>
        /// Find the hashed password from the original one.
        /// </summary>
        /// <param name="originalPassword"></param>
        /// <returns></returns>
        string FindHashedPassword(string originalPassword);

        /// <summary>
        /// Check whether account meets the filter conditions or not.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="filterAccountViewModel"></param>
        /// <returns></returns>
        bool IsAccountConditionMatched(Account account, FilterAccountViewModel filterAccountViewModel);
    }
}