using System.Threading.Tasks;
using Core.Models.Tables;
using Core.ViewModels.Filter;

namespace Core.Interfaces
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
        /// Find the hashed password from the original one.
        /// </summary>
        /// <param name="originalPassword"></param>
        /// <returns></returns>
        string FindHashedPassword(string originalPassword);
    }
}