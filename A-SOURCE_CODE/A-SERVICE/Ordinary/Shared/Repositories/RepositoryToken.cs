using System;
using System.Linq;
using Database.Interfaces;
using Database.Models.Entities;
using Shared.Enumerations;
using Shared.Interfaces.Repositories;
using Shared.ViewModels.Token;

namespace Shared.Repositories
{
    public class RepositoryToken : ParentRepository<Token>, IRepositoryToken
    {
        #region Constructors

        /// <summary>
        ///     Initiate repository with dependency injection.
        /// </summary>
        /// <param name="dbContextWrapper"></param>
        public RepositoryToken(
            IDbContextWrapper dbContextWrapper) : base(dbContextWrapper)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Search tokens with specific conditions.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<Token> Search(IQueryable<Token> tokens, FindTokensViewModel conditions)
        {
            // Owner index are specified.
            if (conditions.OwnerIndexes != null)
                tokens = tokens.Where(x => conditions.OwnerIndexes.Contains(x.OwnerIndex));

            // Types are specified.
            if (conditions.Types != null)
                tokens = tokens.Where(x => conditions.Types.Contains(x.Type));

            // Code has been specified.
            if (conditions.Code != null && !string.IsNullOrEmpty(conditions.Code.Value))
            {
                var szCode = conditions.Code;
                switch (szCode.Mode)
                {
                    case TextComparision.Contain:
                        tokens = tokens.Where(x => x.Code.Contains(szCode.Value));
                        break;
                    case TextComparision.Equal:
                        tokens = tokens.Where(x => x.Code.Equals(szCode.Value));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        tokens =
                            tokens.Where(x => x.Code.Equals(szCode.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextComparision.StartsWith:
                        tokens = tokens.Where(x => x.Code.StartsWith(szCode.Value));
                        break;
                    case TextComparision.StartsWithIgnoreCase:
                        tokens =
                            tokens.Where(
                                x => x.Code.StartsWith(szCode.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case TextComparision.EndsWith:
                        tokens = tokens.Where(x => x.Code.EndsWith(szCode.Value));
                        break;
                    case TextComparision.EndsWithIgnoreCase:
                        tokens =
                            tokens.Where(
                                x => x.Code.EndsWith(szCode.Value, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    default:
                        tokens = tokens.Where(x => x.Code.ToLower().Contains(szCode.Value.ToLower()));
                        break;
                }
            }

            // Issued range is specified.
            if (conditions.Issued != null)
            {
                var issuedRange = conditions.Issued;
                if (issuedRange.From != null)
                    tokens = tokens.Where(x => x.Issued >= issuedRange.From.Value);
                if (issuedRange.To != null)
                    tokens = tokens.Where(x => x.Issued <= issuedRange.To.Value);
            }

            // Expired range is specified.
            if (conditions.Expired != null)
            {
                var expiredRange = conditions.Expired;
                if (expiredRange.From != null)
                    tokens = tokens.Where(x => x.Expired >= expiredRange.From.Value);
                if (expiredRange.To != null)
                    tokens = tokens.Where(x => x.Expired <= expiredRange.To.Value);
            }


            return tokens;
        }

        #endregion
    }
}