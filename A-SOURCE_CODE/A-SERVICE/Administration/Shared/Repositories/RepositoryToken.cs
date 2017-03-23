using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Contextes;
using iConfess.Database.Models.Tables;
using Shared.Enumerations;
using Shared.Enumerations.Order;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Token;

namespace Shared.Repositories
{
    public class RepositoryToken : IRepositoryToken
    {
        #region Properties

        /// <summary>
        ///     Database context which provides access to database.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with dependency injection.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public RepositoryToken(IDbContextWrapper dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete token asynchronously.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public void Delete(FindTokensViewModel conditions)
        {
            // Find tokens with specific conditions.
            var tokens = _dbContextWrapper.Tokens.AsQueryable();
            tokens = Find(tokens, conditions);

            // Delete 'em all.
            _dbContextWrapper.Tokens.RemoveRange(tokens);
        }
        
        /// <summary>
        /// Initiate a token into database.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Token Initiate(Token token)
        {
            _dbContextWrapper.Tokens.AddOrUpdate(token);
            return token;
        }

        /// <summary>
        /// Find tokens with specific conditions.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="findTokensCondition"></param>
        /// <returns></returns>
        public IQueryable<Token> Find(IQueryable<Token> tokens, FindTokensViewModel findTokensCondition)
        {
            // Owner index are specified.
            if (findTokensCondition.OwnerIndexes != null)
                tokens = tokens.Where(x => findTokensCondition.OwnerIndexes.Contains(x.OwnerIndex));

            // Types are specified.
            if (findTokensCondition.Types != null)
                tokens = tokens.Where(x => findTokensCondition.Types.Contains(x.Type));

            // Code has been specified.
            if (findTokensCondition.Code != null && !string.IsNullOrEmpty(findTokensCondition.Code.Value))
            {
                var tokenCodeSearchCondition = findTokensCondition.Code;
                switch (tokenCodeSearchCondition.Mode)
                {
                    case TextComparision.Equal:
                        tokens = tokens.Where(x => x.Code.Equals(tokenCodeSearchCondition.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        tokens =
                            tokens.Where(
                                x =>
                                    x.Code.Equals(tokenCodeSearchCondition.Value,
                                        StringComparison.InvariantCultureIgnoreCase));
                        break;
                    default:
                        tokens = tokens.Where(x => x.Code.Contains(tokenCodeSearchCondition.Value));
                        break;
                }
            }

            // Issued range is specified.
            if (findTokensCondition.Issued != null)
            {
                var issuedRange = findTokensCondition.Issued;
                if (issuedRange.From != null)
                    tokens = tokens.Where(x => x.Issued >= issuedRange.From.Value);
                if (issuedRange.To != null)
                    tokens = tokens.Where(x => x.Issued <= issuedRange.To.Value);
            }

            // Expired range is specified.
            if (findTokensCondition.Expired != null)
            {
                var expiredRange = findTokensCondition.Expired;
                if (expiredRange.From != null)
                    tokens = tokens.Where(x => x.Expired >= expiredRange.From.Value);
                if (expiredRange.To != null)
                    tokens = tokens.Where(x => x.Expired <= expiredRange.To.Value);
            }

            switch (findTokensCondition.Direction)
            {
                case SortDirection.Decending:
                    switch (findTokensCondition.Sort)
                    {
                        case TokenSort.Code:
                            tokens = tokens.OrderByDescending(x => x.Code);
                            break;
                            case TokenSort.Expired:
                            tokens = tokens.OrderByDescending(x => x.Expired);
                            break;
                            case TokenSort.Issued:
                            tokens = tokens.OrderByDescending(x => x.Issued);
                            break;
                            case TokenSort.Type:
                            tokens = tokens.OrderByDescending(x => x.Type);
                            break;
                        default:
                            tokens = tokens.OrderByDescending(x => x.OwnerIndex);
                            break;
                    }
                    break;
                default:
                    switch (findTokensCondition.Sort)
                    {
                        case TokenSort.Code:
                            tokens = tokens.OrderBy(x => x.Code);
                            break;
                        case TokenSort.Expired:
                            tokens = tokens.OrderBy(x => x.Expired);
                            break;
                        case TokenSort.Issued:
                            tokens = tokens.OrderBy(x => x.Issued);
                            break;
                        case TokenSort.Type:
                            tokens = tokens.OrderBy(x => x.Type);
                            break;
                        default:
                            tokens = tokens.OrderBy(x => x.OwnerIndex);
                            break;
                    }
                    break;
            }

            return tokens;
        }

        /// <summary>
        /// Find tokens from database.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Token> Find()
        {
            return _dbContextWrapper.Tokens.AsQueryable();
        }

        #endregion
    }
}