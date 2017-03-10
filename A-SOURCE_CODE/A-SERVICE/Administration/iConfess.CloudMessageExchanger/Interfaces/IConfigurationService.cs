namespace iConfess.CloudMessageExchanger.Interfaces
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Load configuration from file and convert information to specific class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        T LoadConfigurationFromFile<T>(string filePath);
    }
}