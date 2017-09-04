namespace Shared.Models
{
    public class PathInfo
    {
        #region Properties

        /// <summary>
        /// Url of file path.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Whether path is absolute or not.
        /// </summary>
        public bool IsAbsolute { get; set; }

        #endregion
    }
}