﻿// <copyright file="IXmlHarvesterCore{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Core XML Harvester.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IXmlHarvesterCore<TModel>
    {
        /// <summary>
        /// Do harvest algorithm over the specified context.
        /// </summary>
        /// <param name="context">Context to be harvested.</param>
        /// <param name="action">Action of the harvest algorithm.</param>
        /// <returns>Harvest result.</returns>
        Task<TModel> HarvestAsync(XmlNode context, Func<XmlDocument, TModel> action);

        /// <summary>
        /// Do harvest algorithm over the specified context.
        /// </summary>
        /// <param name="context">Context to be harvested.</param>
        /// <param name="actionAsync">Action of the harvest algorithm.</param>
        /// <returns>Harvest result.</returns>
        Task<TModel> HarvestAsync(XmlNode context, Func<XmlDocument, Task<TModel>> actionAsync);
    }
}
