// <copyright file="StyleDataModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Layout
{
    using System;

    /// <summary>
    /// Style data model.
    /// </summary>
    public class StyleDataModel : ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.IIdentifiedStyleDataModel
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
