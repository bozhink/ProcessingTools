// <copyright file="IPostCode.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Geo
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Models.Contracts;

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
