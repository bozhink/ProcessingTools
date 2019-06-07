﻿// <copyright file="Publisher.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Journals
{
    using ProcessingTools.Contracts.Models.Services.Data.Journals;

    /// <summary>
    /// Publisher.
    /// </summary>
    public class Publisher : IPublisher
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }
    }
}
