using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Database.Models.Entities
{
    public class Post
    {
        #region Properties

        /// <summary>
        ///     Id of post.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Who owns the post.
        /// </summary>
        public int OwnerIndex { get; set; }

        /// <summary>
        ///     Which category the post belongs to.
        /// </summary>
        public int CategoryIndex { get; set; }

        /// <summary>
        ///     Title of post.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Post body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     When the post was created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the post was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     Who create the post.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(OwnerIndex))]
        public virtual Account Owner { get; set; }

        /// <summary>
        ///     Category which post belongs to.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(CategoryIndex))]
        public virtual Category Category { get; set; }

        #endregion
    }
}