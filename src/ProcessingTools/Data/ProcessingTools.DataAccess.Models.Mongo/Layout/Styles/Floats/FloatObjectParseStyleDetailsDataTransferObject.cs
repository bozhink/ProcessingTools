﻿// <copyright file="FloatObjectParseStyleDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Models.Mongo.Layout.Styles.Floats
{
    using System;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse style details data transfer object (DTO).
    /// </summary>
    public class FloatObjectParseStyleDetailsDataTransferObject : IFloatObjectDetailsParseStyleDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        public string Script { get; set; }

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType { get; set; }

        /// <inheritdoc/>
        public string FloatObjectXPath { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}