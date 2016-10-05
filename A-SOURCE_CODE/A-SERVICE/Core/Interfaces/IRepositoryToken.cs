using System.Threading.Tasks;
using Core.Models.Tables;
using Core.ViewModels.Filter;

namespace Core.Interfaces
{
    public interface IRepositoryToken
    {
        /// <summary>
        /// Find the first account in the database whose properties match with filter conditions.
        /// </summary>
        /// <param name="filterTokenViewModel"></param>
        /// <returns></returns>
        Task<Token> FindTokenAsync(FilterTokenViewModel filterTokenViewModel);

        /// <summary>
        /// Create an account and save into database asychronously.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Token> InitializeTokenAsync(Token token);
        
        /// <summary>
        /// Edit token information asynchronously.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Token> EditTokenAsync(Token token);
        
        /// <summary>
        /// Activate account by using token.
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        Task ActivateToken(string tokenCode);
    }
}