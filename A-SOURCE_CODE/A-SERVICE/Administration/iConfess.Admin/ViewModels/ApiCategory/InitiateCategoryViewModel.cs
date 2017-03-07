﻿using System.ComponentModel.DataAnnotations;
using Shared.Resources;

namespace iConfess.Admin.ViewModels.ApiCategory
{
    public class InitiateCategoryViewModel
    {
        /// <summary>
        ///     Name of category.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(HttpValidationMessages),
             ErrorMessageResourceName = "InformationRequired")]
        public string Name { get; set; }
    }
}