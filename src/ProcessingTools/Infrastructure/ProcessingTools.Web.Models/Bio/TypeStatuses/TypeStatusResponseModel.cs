// <copyright file="TypeStatusResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Bio.TypeStatuses
{
    using ProcessingTools.Contracts.Models.Bio;

    /// <summary>
    /// Represents response model for the type statuses API.
    /// </summary>
    public class TypeStatusResponseModel : ITypeStatus
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the type status object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the type status object.
        /// </summary>
        public string Name { get; set; }
    }
}
