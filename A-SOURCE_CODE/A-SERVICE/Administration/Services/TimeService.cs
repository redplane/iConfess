using System;
using Administration.Interfaces;

namespace Administration.Services
{
    public class TimeService : ITimeService
    {
        #region Methods

        /// <summary>
        /// Convert unix time to Utc datetime.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public DateTime UnixToUtc(double unixTime)
        {
            return _utcDateTime.AddMilliseconds(unixTime);
        }

        /// <summary>
        /// Convert datetime to utc.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public double UtcToUnix(DateTime dateTime)
        {
            return (dateTime - _utcDateTime).TotalMilliseconds;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Original utc datetime.
        /// </summary>
        private readonly DateTime _utcDateTime;

        /// <summary>
        /// Initialize an instance with default settings.
        /// </summary>
        public TimeService()
        {
            _utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        #endregion
    }
}