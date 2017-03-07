using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Accounts;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryAccount : IParentRepository<Account>
    {
        /// <summary>
        ///     Create / update an account asynchronously with specific conditions.
        /// </summary>
        /// <returns></returns>
        /// <param name="account"></param>
        Account Initiate(Account account);

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
        void Delete(FindAccountsViewModel conditions);

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
        IQueryable<Account> Find(IQueryable<Account> accounts, FindAccountsViewModel conditions);
    }
}