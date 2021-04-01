﻿// <copyright file="IFlora.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Special
{
    using System.Xml;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Flora.
    /// </summary>
    public interface IFlora
    {
        /// <summary>
        /// Parse infra-ranks.
        /// </summary>
        /// <param name="document">Document context.</param>
        void ParseInfra(IDocument document);

        /// <summary>
        /// Parse taxon names.
        /// </summary>
        /// <param name="document">Document context.</param>
        /// <param name="template">Template for taxon names.</param>
        void ParseTn(IDocument document, XmlDocument template);

        /// <summary>
        /// Performs replace.
        /// </summary>
        /// <param name="document">Document context.</param>
        /// <param name="template">Template for taxon names.</param>
        void PerformReplace(IDocument document, XmlDocument template);
    }
}
