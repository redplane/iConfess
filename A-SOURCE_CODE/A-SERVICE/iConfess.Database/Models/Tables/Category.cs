namespace iConfess.Database.Models.Tables
{
    public class Category
    {
        #region Properties

        /// <summary>
        /// Id of category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Who created the current category.
        /// </summary>
        public int CreatorIndex { get; set; }

        /// <summary>
        /// Name of category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// When the category was created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        /// When the category was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        /// One category can only be created by one account.
        /// </summary>
        public Account Creator { get; set; }
        
        /// <summary>
        /// Relationship between the account following this category with this one.
        /// </summary>
        public FollowCategory BeFollowed { get; set; }

        #endregion

    }
}