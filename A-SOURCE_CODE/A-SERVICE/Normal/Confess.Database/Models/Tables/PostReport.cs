﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Confess.Database.Models.Tables
{
    public class PostReport
    {
        #region Properties

        /// <summary>
        ///     Id of report.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Which post is reported.
        /// </summary>
        public int PostIndex { get; set; }

        /// <summary>
        ///     Who owns the post.
        /// </summary>
        public int PostOwnerIndex { get; set; }

        /// <summary>
        ///     Who report the post.
        /// </summary>
        public int PostReporterIndex { get; set; }

        /// <summary>
        ///     Original content of post.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///     Reason the post was reported.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        ///     When the report was created.
        /// </summary>
        public double Created { get; set; }

        #endregion

        #region Relationships

        /// <summary>
        ///     One report is about one post, just one.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(PostIndex))]
        public Post Post { get; set; }

        /// <summary>
        ///     Report can only be about one account.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(PostOwnerIndex))]
        public Account PostOwner { get; set; }

        /// <summary>
        ///     Report can only belong to one account.
        /// </summary>
        [JsonIgnore]
        [ForeignKey(nameof(PostReporterIndex))]
        public Account PostReporter { get; set; }

        #endregion
    }
}