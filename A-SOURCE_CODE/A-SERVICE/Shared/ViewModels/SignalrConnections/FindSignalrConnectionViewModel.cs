﻿using Shared.Models;

namespace Shared.ViewModels.SignalrConnections
{
    public class FindSignalrConnectionViewModel
    {
        #region Properties

        /// <summary>
        /// Index of connection.
        /// </summary>
        public TextSearch Index { get; set; }

        /// <summary>
        /// Owner of connection.
        /// </summary>
        public int? Owner { get; set; }

        /// <summary>
        /// Range of created time.
        /// </summary>
        public UnixDateRange Created { get; set; }

        #endregion
    }
}