namespace iConfess.Database.Models.Tables
{
    public class ReportedPost
    {
        /// <summary>
        /// Id of report.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Which post is reported.
        /// </summary>
        public int Post { get; set; }

        /// <summary>
        /// Who owns the post.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// Who report the post.
        /// </summary>
        public int Reporter { get; set; }

        /// <summary>
        /// Original content of post.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Reason the post was reported.
        /// </summary>
        public string Reason { get; set; }
        
        /// <summary>
        /// When the report was created.
        /// </summary>
        public double Created { get; set; }

    }
}