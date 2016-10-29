using Administration.Enumerations;
using Administration.ViewModels.Filter;
using Microsoft.AspNetCore.Authorization;

namespace Administration.Requirements
{
    public class AccountRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// List of required statuses.
        /// </summary>
        private readonly FilterAccountViewModel _filterAccountViewModel;

        /// <summary>
        /// List of statuses which are allowed to access function/class.
        /// </summary>
        public FilterAccountViewModel FilterAccountViewModel
        {
            get { return _filterAccountViewModel; }
        }

        /// <summary>
        /// Message describes about error.
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Initialize requirement with specfic conditions.
        /// </summary>
        /// <param name="filterAccountViewModel">Conditions which account must match with.</param>
        /// <param name="responseMessage"></param>
        public AccountRequirement(FilterAccountViewModel filterAccountViewModel, string responseMessage)
        {
            _filterAccountViewModel = filterAccountViewModel;
            ResponseMessage = responseMessage;
        }

        /// <summary>
        /// Initialize requirement with specific conditions.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="emailComparision"></param>
        /// <param name="nickname"></param>
        /// <param name="nicknameComparision"></param>
        /// <param name="password"></param>
        /// <param name="passwordComparision"></param>
        /// <param name="statuses"></param>
        /// <param name="minCreated"></param>
        /// <param name="maxCreated"></param>
        /// <param name="minLastModified"></param>
        /// <param name="maxLastModified"></param>
        public AccountRequirement(string email, TextComparision emailComparision, string nickname,
            TextComparision nicknameComparision, string password, TextComparision passwordComparision,
            AccountStatus[] statuses, double? minCreated, double? maxCreated, double? minLastModified,
            double? maxLastModified)
        {
            _filterAccountViewModel = new FilterAccountViewModel
            {
                Email = email,
                EmailComparison = emailComparision,
                Nickname = nickname,
                NicknameComparision = nicknameComparision,
                Password = password,
                PasswordComparision = passwordComparision,
                Statuses = statuses,
                MinCreated = minCreated,
                MaxCreated = maxCreated,
                MinLastModified = minLastModified,
                MaxLastModified = maxLastModified
            };
        }
    }
}