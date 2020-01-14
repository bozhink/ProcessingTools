﻿// <copyright file="TypeStatus.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio
{
    using ProcessingTools.Contracts.Models.Bio;

    /// <summary>
    /// Type status service model.
    /// </summary>
    public class TypeStatus : ITypeStatus
    {
        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the type status.
        /// </summary>
        public string Name { get; set; }
    }
}
