// <copyright file="IHaveCustomMappings.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    using AutoMapper;

    /// <summary>
    /// Model with custom mappings.
    /// </summary>
    public interface IHaveCustomMappings
    {
        /// <summary>
        /// Create custom configuration for Automapper.
        /// </summary>
        /// <param name="configuration">Configuration to be updated.</param>
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
