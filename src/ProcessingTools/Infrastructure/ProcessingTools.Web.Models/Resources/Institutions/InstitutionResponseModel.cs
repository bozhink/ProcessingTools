// <copyright file="InstitutionResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Resources.Institutions
{
    using ProcessingTools.Models.Contracts.Resources;

    /// <summary>
    /// Represents response model for the institutions API.
    /// </summary>
    public class InstitutionResponseModel : IInstitution
    {
        /// <summary>
        /// Gets or sets the Identifier (ID) of the institution object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name of the institution object.
        /// </summary>
        public string Name { get; set; }
    }
}
