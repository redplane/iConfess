using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Enumerations;
using Core.Interfaces;
using Core.Models.Tables;
using Core.ViewModels.Filter;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class RepositoryToken : IRepositoryToken
    {
        /// <summary>
        /// Database context wrapper.
        /// </summary>
        private readonly MainDbContext _mainDbContext;

        /// <summary>
        /// Initialize an instance of repository with dependency injections.
        /// </summary>
        /// <param name="mainDbContext"></param>
        public RepositoryToken(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        /// <summary>
        /// Find the first account in the database whose properties match with filter conditions.
        /// </summary>
        /// <param name="filterTokenViewModel"></param>
        /// <returns></returns>
        public async Task<Token> FindTokenAsync(FilterTokenViewModel filterTokenViewModel)
        {
            // Take all accounts from database first.
            IQueryable<Token> tokens = _mainDbContext.Tokens;

            // Do account filtering.
            tokens = FilterTokens(tokens, filterTokenViewModel);

            // Take the first found account in the list.
            return await tokens.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create an account and save into database asychronously.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Token> InitializeTokenAsync(Token token)
        {
            // Delete the same token first.
            using (var transaction = await _mainDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var copiedToken = new Token();
                    copiedToken.Type = token.Type;
                    copiedToken.Owner = token.Owner;

                    IQueryable<Token> tokens = _mainDbContext.Tokens;
                    tokens = tokens.Where(x => x.Type == copiedToken.Type);
                    tokens = tokens.Where(x => x.Owner == copiedToken.Owner);

                    // Remove the same type tokens first.
                    _mainDbContext.Tokens.RemoveRange(tokens);

                    // Add new token
                    token = _mainDbContext.Add(token).Entity;

                    // Save changes asychronously.
                    await _mainDbContext.SaveChangesAsync();

                    // Commit the transaction.
                    transaction.Commit();

                    return token;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
                
        }

        /// <summary>
        /// Edit token information asynchronously.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Token> EditTokenAsync(Token token)
        {
            var tokenEntry = _mainDbContext.Entry(token);
            tokenEntry.State = EntityState.Modified;

            // Save the changes asynchronously.
            await _mainDbContext.SaveChangesAsync();

            token = _mainDbContext.Entry(token).Entity;
            return token;
        }

        /// <summary>
        /// Activate account by using token.
        /// </summary>
        /// <param name="tokenCode"></param>
        /// <returns></returns>
        public async Task ActivateToken(string tokenCode)
        {
            using (var transaction = _mainDbContext.Database.BeginTransaction())
            {
                try
                {
                    // Find the token first.
                    IQueryable<Token> tokens = _mainDbContext.Tokens;
                    tokens = tokens.Where(x => x.Code.Equals(tokenCode, StringComparison.Ordinal));
                    tokens = tokens.Where(x => x.Type == TokenType.Activation);
                    
                    // Find the pending account related to token.
                    IQueryable<Account> accounts = _mainDbContext.Accounts;
                    accounts = accounts.Where(x => x.Status == AccountStatus.Pending);

                    // Find the first account which matches with the token.
                    var account = await (from a in accounts
                        from t in tokens
                        where a.Id == t.Owner
                        select a).FirstAsync();
                    
                    // Modify the account status to active.
                    account.Status = AccountStatus.Active;
                    _mainDbContext.Entry(account).State = EntityState.Modified;
                    _mainDbContext.Entry(tokens).State = EntityState.Deleted;

                    // Save changes in database.
                    var changes = await _mainDbContext.SaveChangesAsync();
                    if (changes != 2)
                        throw new Exception("INVALID_ACTIVATION");

                    // Commit the transaction.
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
        /// <summary>
        /// Filter accounts by using specific conditions.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="filterTokenViewModel"></param>
        /// <returns></returns>
        private IQueryable<Token> FilterTokens(IQueryable<Token> tokens,
            FilterTokenViewModel filterTokenViewModel)
        {
            // Id is specified.
            if (filterTokenViewModel.Id != null)
                tokens = tokens.Where(x => x.Id == filterTokenViewModel.Id.Value);

            // Owner id is specified.
            if (filterTokenViewModel.Owner != null)
                tokens = tokens.Where(x => x.Owner == filterTokenViewModel.Owner.Value);
            
            // Email is specified.
            if (!string.IsNullOrWhiteSpace(filterTokenViewModel.Code))
            {
                switch (filterTokenViewModel.CodeComparision)
                {
                    case TextComparision.Contain:
                        tokens = tokens.Where(x => x.Code.Contains(filterTokenViewModel.Code));
                        break;
                    case TextComparision.EqualIgnoreCase:
                        tokens =
                            tokens.Where(
                                x =>
                                    x.Code.Equals(filterTokenViewModel.Code,
                                        StringComparison.CurrentCultureIgnoreCase));
                        break;
                    default:
                        tokens = tokens.Where(x => x.Code.Equals(filterTokenViewModel.Code));
                        break;
                }
            }
            
            // Types are specified.
            if (filterTokenViewModel.Types != null)
            {
                // Build the types as a list.
                var types = new List<TokenType>(filterTokenViewModel.Types);
                tokens = tokens.Where(x => types.Contains(x.Type));
            }

            // Expiration time is specified.
            if (filterTokenViewModel.MinExpire != null)
                tokens = tokens.Where(x => x.Expire >= filterTokenViewModel.MinExpire.Value);
            if (filterTokenViewModel.MaxExpire != null)
                tokens = tokens.Where(x => x.Expire <= filterTokenViewModel.MaxExpire.Value);
            
            return tokens;
        }
    }
}