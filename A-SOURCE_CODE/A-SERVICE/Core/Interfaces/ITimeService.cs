using System;

namespace Core.Interfaces
{
    public interface ITimeService
    {
        /// <summary>
        /// Convert unix time to Utc.
        /// </summary>
        /// <returns></returns>
        DateTime UnixToUtc(double unix);

        /// <summary>
        /// Convert Utc time to DateTime.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        double UtcToUnix(DateTime dateTime);
    }
}
