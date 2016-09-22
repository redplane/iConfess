using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Tables
{
    [Table(nameof(FollowCategory))]
    public class FollowCategory
    {
        #region Properties

        /// <summary>
        /// Who is following category.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// Id of category which account is following.
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// When the follow started.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Foreign keys

        /// <summary>
        /// Information of owner.
        /// </summary>
        public virtual Account OwnerDetail { get; set; }
        
        /// <summary>
        /// Information of category.
        /// </summary>
        public virtual Category CategoryDetail { get; set; }

        #endregion
    }
}
