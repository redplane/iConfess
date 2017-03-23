using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Token;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryToken
    {
        /// <summary>
        /// Initiate / update token into database.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Token Initiate(Token token);

        /// <summary>
        /// Delete token from database.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        void Delete(FindTokensViewModel conditions);
        
        /// <summary>
        /// Find tokens using specific conditions.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Token> Find(IQueryable<Token> tokens, FindTokensViewModel conditions);

        /// <summary>
        /// Find all tokens from database.
        /// </summary>
        /// <returns></returns>
        IQueryable<Token> Find();
    }
}