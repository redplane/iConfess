using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace iConfess.Database.Models.Tables
{
    public class Token
    {
        #region Properties

        /// <summary>
        ///     Id of token.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Who this token belongs to.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        ///     Code of token
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Type of Token.
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        ///     When the token expires.
        /// </summary>
        public System.DateTime Expire { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     One category have one owner.
        /// </summary>
        [JsonIgnore]
        public Account Owner { get; set; }

        #endregion
    }
}
