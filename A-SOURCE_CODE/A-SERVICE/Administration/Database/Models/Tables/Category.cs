using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Database.Models.Tables
{
    public class Category
    {
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

        #region Relationships

        /// <summary>
        ///     One category can only be created by one account.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(CreatorIndex))]
        public Account Creator { get; set; }

        /// <summary>
        ///     Relationship between the account following this category with this one.
        /// </summary>
        [JsonIgnore]
        public ICollection<FollowCategory> FollowCategories { get; set; }

        /// <summary>
        ///     One category can contains many posts.
        /// </summary>
        [JsonIgnore]
        public ICollection<Post> Posts { get; set; }

        #endregion
    }
}