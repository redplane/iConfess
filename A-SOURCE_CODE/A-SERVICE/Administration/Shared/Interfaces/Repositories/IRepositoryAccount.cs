﻿using System.Linq;
using iConfess.Database.Models.Tables;
using Shared.ViewModels.Accounts;

namespace Shared.Interfaces.Repositories
{
    public interface IRepositoryAccount : IParentRepository<Account>
    {
        /// <summary>
        ///     Search accounts using specific conditions.
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        IQueryable<Account> Search(IQueryable<Account> accounts, SearchAccountViewModel conditions);
    }
}