// <copyright file="InstitutionDataModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Journals
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Journals;

    /// <summary>
    /// Institution data model.
    /// </summary>
    public class InstitutionDataModel : IInstitution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionDataModel"/> class.
        /// </summary>
        public InstitutionDataModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Addresses = new HashSet<IAddress>();
        }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IAddress> Addresses { get; set; }
    }
}
