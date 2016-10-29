namespace Administration.Interfaces
{
    public interface IPagination
    {
        /// <summary>
        /// Index of page record.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        /// Number of records which can be displayed on a page.
        /// </summary>
        int Records { get; set; }
    }
}