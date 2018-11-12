// <copyright file="StyleDataModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Layout.Mongo
{
    using System;

    /// <summary>
    /// Style data model.
    /// </summary>
    public class StyleDataModel : ProcessingTools.Data.Models.Contracts.Layout.Styles.IIdentifiedStyleDataModel
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
