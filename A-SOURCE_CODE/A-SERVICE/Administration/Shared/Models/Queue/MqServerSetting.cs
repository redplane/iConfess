namespace Shared.Models.Queue
{
    public class MqServerSetting
    {
        #region Properties

        /// <summary>
        /// Queue server hostname.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Username which is used for accessing into queue server.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password which is used for accessing into queue server.
        /// </summary>
        public string Password { get; set; }

        #endregion
    }
}