﻿namespace Shared.Interfaces.Services
{
    public interface IFileService
    {
        #region Methods

        /// <summary>
        /// Load configuration from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isAbsolute"></param>
        /// <returns></returns>
        T LoadFileConfiguration<T>(string path, bool isAbsolute);

        #endregion
    }
}