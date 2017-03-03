using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using iConfess.Database.Models;
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
        private readonly ConfessDbContext _iConfessDbContext;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initiate repository with dependency injection.
        /// </summary>
        /// <param name="iConfessDbContext"></param>
        public RepositoryToken(ConfessDbContext iConfessDbContext)
        {
            _iConfessDbContext = iConfessDbContext;
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
            var tokens = _iConfessDbContext.Tokens.AsQueryable();
            tokens = FindTokens(tokens, conditions);

            // Delete 'em all.
            _iConfessDbContext.Tokens.RemoveRange(tokens);
        }

        /// <summary>
        /// Find token asychronously using specific conditions.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public Task<Token> FindTokenAsync(FindTokensViewModel conditions)
        {
            // Result initialization.
            var findTokensResultViewModel = new FindTokensResultViewModel();

            // Token search.
            var tokens = _iConfessDbContext.Tokens.AsQueryable();
            tokens = FindTokens(tokens, conditions);
            return tokens.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find tokens by using specific conditions asynchronously.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public async Task<FindTokensResultViewModel> FindTokensAsync(FindTokensViewModel conditions)
        {
            // Result initialization.
            var findTokensResultViewModel = new FindTokensResultViewModel();

            // Token search.
            var tokens = _iConfessDbContext.Tokens.AsQueryable();
            tokens = FindTokens(tokens, conditions);

            // Count the number of matched records.
            findTokensResultViewModel.Total = await tokens.CountAsync();

            // Do pagination.
            if (conditions.Pagination != null)
            {
                var pagination = conditions.Pagination;
                tokens = tokens.Skip(pagination.Index*pagination.Records).Take(pagination.Records);
            }

            
            findTokensResultViewModel.Tokens = tokens;
            return findTokensResultViewModel;
        }

        /// <summary>
        /// Initiate a token into database.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Token Initiate(Token token)
        {
            _iConfessDbContext.Tokens.AddOrUpdate(token);
            return token;
        }

        /// <summary>
        /// Find tokens with specific conditions.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="findTokensCondition"></param>
        /// <returns></returns>
        public IQueryable<Token> FindTokens(IQueryable<Token> tokens, FindTokensViewModel findTokensCondition)
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
        public IQueryable<Token> FindTokens()
        {
            return _iConfessDbContext.Tokens.AsQueryable();
        }

        #endregion
    }
}