using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Administration.Enumerations;

namespace Administration.Models.Tables
{
    public class Token
    {
        #region Properties

        /// <summary>
        /// Id of token, this must be unique.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id of account which owns this token.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// Token code.
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Type of token
        /// </summary>
        public TokenType Type { get; set; }

        /// <summary>
        /// When the token should be expired.
        /// </summary>
        public double Expire { get; set; }

        #endregion

        #region Relationship

        /// <summary>
        /// Detail information of account which owns this token.
        /// </summary>
        [ForeignKey(nameof(Owner))]
        public virtual Account OwnerDetail { get; set; }

        #endregion
    }
}