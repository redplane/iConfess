using System.Collections.Generic;

namespace Administration.Models.Tables
{
    public class Category
    {
        #region Properties

        /// <summary>
        ///     Id of category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Category name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     When the category was created on service.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the category was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     List of following relationships.
        /// </summary>
        public virtual ICollection<FollowCategory> BeingFollowed { get; set; }

        /// <summary>
        ///     List of posts which category contains.
        /// </summary>
        public virtual ICollection<Post> ContainPosts { get; set; }

        #endregion
    }
}