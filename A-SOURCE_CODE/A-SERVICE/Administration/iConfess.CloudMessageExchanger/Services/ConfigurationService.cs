using System;
using System.IO;
using iConfess.CloudMessageExchanger.Interfaces;
using Newtonsoft.Json;

namespace iConfess.CloudMessageExchanger.Services
{
    /// <summary>
    /// Class defines methods to access configuration file.
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// Load configuration from file and serialize information to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public T LoadConfigurationFromFile<T>(string filePath)
        {
            // File doesn't exist.
            if (!File.Exists(filePath))
                return default(T);

            var fileContent = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(fileContent);
        }
    }
}