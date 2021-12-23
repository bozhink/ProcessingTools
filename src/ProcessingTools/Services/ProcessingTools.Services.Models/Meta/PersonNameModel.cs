﻿// <copyright file="PersonNameModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Meta
{
    using ProcessingTools.Contracts.Models.Meta;

    /// <summary>
    /// Person name model.
    /// </summary>
    public class PersonNameModel : IPersonNameModel
    {
        /// <inheritdoc/>
        public string GivenNames { get; set; }

        /// <inheritdoc/>
        public string Prefix { get; set; }

        /// <inheritdoc/>
        public string Suffix { get; set; }

        /// <inheritdoc/>
        public string Surname { get; set; }
    }
}
