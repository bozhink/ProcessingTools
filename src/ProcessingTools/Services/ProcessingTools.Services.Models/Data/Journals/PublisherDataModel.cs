// <copyright file="PublisherDataModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Journals
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Journals;

    /// <summary>
    /// Publisher data model.
    /// </summary>
    public class PublisherDataModel : IPublisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherDataModel"/> class.
        /// </summary>
        public PublisherDataModel()
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
