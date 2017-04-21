using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Database.Models.Entities
{
    public class Category
    {
        #region Relationships

        /// <summary>
        ///     One category can only be created by one account.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(CreatorIndex))]
        public virtual Account Creator { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Id of category.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Who created the current category.
        /// </summary>
        public int CreatorIndex { get; set; }

        /// <summary>
        ///     Name of category.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     When the category was created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the category was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion
    }
}