// <copyright file="IPostCode.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Geo
{
    using ProcessingTools.Common.Enumerations;

    /// <summary>
    /// Post code.
    /// </summary>
    public interface IPostCode : IIntegerIdentified
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
