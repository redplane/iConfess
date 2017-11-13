namespace Administration.ViewModels.ApiPost
{
    public class DailyPostSummaryViewModel : PostSummaryViewModel
    {
        /// <summary>
        /// How many days should summary be calculated.
        /// </summary>
        public int Days { get; set; }
    }
}