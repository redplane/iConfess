using System;
using Confess.Ordinary.Interfaces.Services;

namespace Confess.Ordinary.Services
{
    public class TimeService : ITimeService
    {
        /// <summary>
        /// Convert UTC DateTime to Unix time.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public double DateTimeUtcToUnix(DateTime dateTime)
        {
            return (dateTime - _utcDateTime).TotalMilliseconds;
        }

        /// <summary>
        /// Convert Unix time to DateTime UTC.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public DateTime UnixToDateTimeUtc(double unixTime)
        {
            return _utcDateTime.AddMilliseconds(unixTime);
        }

        #region Properties

        /// <summary>
        /// Original UTC DateTime.
        /// </summary>
        private readonly DateTime _utcDateTime;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate service with default settings.
        /// </summary>
        public TimeService()
        {
            _utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        #endregion
    }
}