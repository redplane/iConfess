using System.ComponentModel.DataAnnotations.Schema;

namespace SystemDatabase.Models.Entities
{
    public class Categorization
    {
        #region Properties

        /// <summary>
        /// Id of category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Id of post.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Time when post was categorized.
        /// </summary>
        public double CategorizationTime { get; set; }

        #endregion

        #region Relationship

        /// <summary>
        /// Category information.
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        /// <summary>
        /// Post information.
        /// </summary>
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        #endregion
    }
}