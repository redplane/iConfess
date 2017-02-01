using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Accounts;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryAccount
    {
        /// <summary>
        ///     Create / update an account asynchronously with specific conditions.
        /// </summary>
        /// <returns></returns>
        /// <param name="account"></param>
        Task<Account> InitiateAccountAsync(Account account);

        /// <summary>
        ///     Find accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        /// <param name="conditions"></param>
        Task<ResponseAccountsViewModel> FindAccountsAsync(FindAccountsViewModel conditions);

        /// <summary>
        ///     Delete accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        /// <param name="conditions"></param>
        Task<int> DeleteAccountsAsync(FindAccountsViewModel conditions);

        /// <summary>
        /// Find account by using specific conditions.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        Task<Account> FindAccountAsync(FindAccountsViewModel conditions);

        /// <summary>
        /// Find accounts using specific conditions.
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Account> FindAccounts(IQueryable<Account> accounts, FindAccountsViewModel conditions);

        /// <summary>
        /// Find all accounts in database.
        /// </summary>
        /// <returns></returns>
        IQueryable<Account> FindAccounts();
    }
}