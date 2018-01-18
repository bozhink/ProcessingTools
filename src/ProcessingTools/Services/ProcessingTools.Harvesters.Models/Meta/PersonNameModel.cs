// <copyright file="PersonNameModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Models.Meta
{
    using ProcessingTools.Contracts.Models.Harvesters.Meta;

    /// <summary>
    /// Person Name Model
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
