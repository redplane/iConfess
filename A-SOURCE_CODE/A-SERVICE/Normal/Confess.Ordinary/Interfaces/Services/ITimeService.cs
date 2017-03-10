using System;

namespace Confess.Ordinary.Interfaces.Services
{
    public interface ITimeService
    {
        /// <summary>
        /// Convert UTC DateTime to Unix time.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        double DateTimeUtcToUnix(DateTime dateTime);

        /// <summary>
        /// Convert Unix time to DateTime UTC.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        DateTime UnixToDateTimeUtc(double unixTime);
    }
}