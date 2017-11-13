using System.IO;
using System.Web;
using Newtonsoft.Json;
using Shared.Interfaces.Services;

namespace Administration.Services
{
    public class FileService : IFileService
    {
        #region Methods

        /// <summary>
        ///     Load configuration from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isAbsolute"></param>
        /// <returns></returns>
        public T LoadFileConfiguration<T>(string path, bool isAbsolute)
        {
            // Path is empty.
            if (string.IsNullOrWhiteSpace(path))
                return default(T);

            // Path is not absolute.
            if (!isAbsolute)
                path = HttpContext.Current.Server.MapPath(path);

            // File doesn't exist.
            if (!File.Exists(path))
                return default(T);

            // Read all text in path.
            var info = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(info);
        }

        #endregion
    }
}