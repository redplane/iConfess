using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enumerations;
using Newtonsoft.Json;

namespace Entities.Models.Entities
{
    public class Account
    {
        #region Properties

        /// <summary>
        ///     Index of account (Auto incremented)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Email which is used for account registration.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Nickname of account owner.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        ///     Password of account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Account status in the system.
        /// </summary>
        public Statuses Status { get; set; }

        /// <summary>
        ///     Role of account
        /// </summary>
        public Roles Role { get; set; }

        /// <summary>
        ///     Relative url (http url) of user photo.
        /// </summary>
        public string PhotoRelativeUrl { get; set; }

        /// <summary>
        ///     Physical path of photo on the server.
        ///     This parameter should be ignored when data is sent back to client.
        /// </summary>
        [JsonIgnore]
        public string PhotoAbsoluteUrl { get; set; }

        /// <summary>
        ///     When was the account created.
        /// </summary>
        public double Joined { get; set; }

        /// <summary>
        ///     When the account was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion
    }
}