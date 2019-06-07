﻿// <copyright file="InstitutionRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Resources.Institutions
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.DataResources;
    using ProcessingTools.Contracts.Models.Resources;

    /// <summary>
    /// Represents request model for the institutions API.
    /// </summary>
    public class InstitutionRequestModel : IInstitution
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the institution object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the institution object.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.InstitutionNameMaximalLength)]
        public string Name { get; set; }
    }
}
