namespace iConfess.Admin.ViewModels.ApiCategory
{
    public class InitiateCategoryViewModel
    {
        /// <summary>
        ///     Id of category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Who created the current category.
        /// </summary>
        public int CreatorIndex { get; set; }

        /// <summary>
        ///     Name of category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     When the category was created.
        /// </summary>
        public double Created { get; set; }

        /// <summary>
        ///     When the category was lastly modified.
        /// </summary>
        public double? LastModified { get; set; }
    }
}