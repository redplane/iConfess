using Administration.Enumerations;
using Administration.Interfaces;

namespace Administration.ViewModels.Filter
{
    public class FilterAccountViewModel : IPagination
    {
        /// <summary>
        /// Id of account.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Email which is used for registering account.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email information comparision mode.
        /// </summary>
        public TextComparision EmailComparison { get; set; }

        /// <summary>
        /// Nickname of the account (display name)
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Nickname information comparision mode.
        /// </summary>
        public TextComparision NicknameComparision { get; set; }

        /// <summary>
        /// Encrypted password of account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Encrypted password comparision mode.
        /// </summary>
        public TextComparision PasswordComparision { get; set; }

        /// <summary>
        /// List of statuses which are used for being filtered.
        /// </summary>
        public AccountStatus[] Statuses { get; set; }

        /// <summary>
        /// List of roles which are used for being filtered.
        /// </summary>
        public AccountRole [] Roles { get; set; }

        /// <summary>
        /// Time when the created must be after.
        /// </summary>
        public double? MinCreated { get; set; }

        /// <summary>
        /// Time when the create must be before.
        /// </summary>
        public double? MaxCreated { get; set; }

        /// <summary>
        /// Time when the last modified must be after.
        /// </summary>
        public double? MinLastModified { get; set; }

        /// <summary>
        /// Time when the last modified must be before.
        /// </summary>
        public double? MaxLastModified { get; set; }

        /// <summary>
        /// Index of result page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of records which can be displayed on a page.
        /// </summary>
        public int Records { get; set; }
    }
}