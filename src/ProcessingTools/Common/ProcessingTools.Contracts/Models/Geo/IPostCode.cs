// <copyright file="IPostCode.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Post code.
    /// </summary>
    public interface IPostCode : IIntegerIdentifiable
    {
        /// <summary>
        /// Gets post code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets post code type.
        /// </summary>
        PostCodeType Type { get; }

        /// <summary>
        /// Gets city ID.
        /// </summary>
        int CityId { get; }
    }
}
