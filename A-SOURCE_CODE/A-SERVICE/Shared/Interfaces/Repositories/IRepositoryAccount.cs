using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryAccount
    {
        /// <summary>
        /// Create / update an account asynchronously with specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<Account> InitiateAccountAsync();

        /// <summary>
        /// Find accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<Account>> FindAccountsAsync();
        
        /// <summary>
        /// Delete accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        Task<int> DeleteAccountsAsync();
    }
}