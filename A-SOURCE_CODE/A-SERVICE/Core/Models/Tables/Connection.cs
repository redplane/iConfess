﻿namespace Core.Models.Tables
{
    public class Connection
    {
        #region Relationship

        /// <summary>
        ///     Connection owner.
        /// </summary>
        public virtual Account OwnerDetail { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Index of connection which generated by SignalR
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     Owner of connection.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        ///     When the connection was logged onto server.
        /// </summary>
        public double Created { get; set; }

        #endregion
    }
}