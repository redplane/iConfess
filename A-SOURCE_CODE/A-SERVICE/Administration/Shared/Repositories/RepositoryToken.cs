using System.Linq;
using iConfess.Database.Interfaces;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Repositories;
using Shared.Interfaces.Services;
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
        /// <param name="commonRepositoryService"></param>
        public RepositoryToken(
            IDbContextWrapper dbContextWrapper,
            ICommonRepositoryService commonRepositoryService) : base(dbContextWrapper)
        {
            _dbContextWrapper = dbContextWrapper;
            _commonRepositoryService = commonRepositoryService;
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
                tokens = _commonRepositoryService.SearchPropertyText(tokens, x => x.Code, conditions.Code);

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

        #region Properties

        /// <summary>
        ///     Database context which provides access to database.
        /// </summary>
        private readonly IDbContextWrapper _dbContextWrapper;

        /// <summary>
        ///     Service which handles common repositories businesses.
        /// </summary>
        private readonly ICommonRepositoryService _commonRepositoryService;

        #endregion
    }
}