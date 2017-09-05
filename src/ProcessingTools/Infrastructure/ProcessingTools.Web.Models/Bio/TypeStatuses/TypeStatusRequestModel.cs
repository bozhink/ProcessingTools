// <copyright file="TypeStatusRequestModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.TypeStatuses
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Bio;
    using ProcessingTools.Contracts.Models.Bio;

    /// <summary>
    /// Represents request model for the type statuses API.
    /// </summary>
    public class TypeStatusRequestModel : ITypeStatus
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the type status object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the type status object.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfTypeStatusName)]
        public string Name { get; set; }
    }
}
