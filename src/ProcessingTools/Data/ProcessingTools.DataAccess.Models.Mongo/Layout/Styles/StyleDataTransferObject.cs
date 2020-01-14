// <copyright file="StyleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Models.Mongo.Layout.Styles
{
    using System;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;

    /// <summary>
    /// Style data model.
    /// </summary>
    public class StyleDataTransferObject : IIdentifiedStyleDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
