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
    }
}