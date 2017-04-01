using System.Linq;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Token;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryToken : IParentRepository<Token>
    {
        /// <summary>
        ///     Search tokens using specific conditions.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Token> Search(IQueryable<Token> tokens, FindTokensViewModel conditions);
    }
}