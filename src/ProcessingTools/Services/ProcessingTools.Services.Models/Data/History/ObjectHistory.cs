// <copyright file="ObjectHistory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.History
{
    using System;
    using ProcessingTools.Models.Contracts.Services.Data.History;

    /// <summary>
    /// History item service model.
    /// </summary>
    public class ObjectHistory : IObjectHistory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectHistory"/> class.
        /// </summary>
        public ObjectHistory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public string Data { get; set; }

        /// <inheritdoc/>
        public string ObjectId { get; set; }

        /// <inheritdoc/>
        public string ObjectType { get; set; }

        /// <inheritdoc/>
        public string AssemblyName { get; set; }

        /// <inheritdoc/>
        public string AssemblyVersion { get; set; }

        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }
    }
}
