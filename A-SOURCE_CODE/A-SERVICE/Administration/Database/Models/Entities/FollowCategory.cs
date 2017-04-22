using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Database.Models.Entities
{
    public class FollowCategory
    {
        #region Properties
        
        /// <summary>
        ///     Owner of following relationship.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        ///     Category index.
        /// </summary>
        public int CategoryIndex { get; set; }

        /// <summary>
        ///     When the relationship was lastly created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     Who starts watching.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(OwnerIndex))]
        public Account Owner { get; set; }

        /// <summary>
        ///     Which is being watched.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(CategoryIndex))]
        public Category Category { get; set; }

        #endregion
    }
}