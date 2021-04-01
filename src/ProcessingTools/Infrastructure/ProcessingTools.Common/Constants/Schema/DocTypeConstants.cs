﻿// <copyright file="DocTypeConstants.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Constants.Schema
{
    using ProcessingTools.Common.Constants.Configuration;

    /// <summary>
    /// Constants related to DOCTYPE-s.
    /// </summary>
    public static class DocTypeConstants
    {
        /// <summary>
        /// TaxPub element name.
        /// </summary>
        public const string TaxPubName = "article";

        /// <summary>
        /// TaxPub public id.
        /// </summary>
        public const string TaxPubPublicId = "-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN";

        /// <summary>
        /// TaxPub system id.
        /// </summary>
        public const string TaxPubSystemId = "tax-treatment-NS0.dtd";

        /// <summary>
        /// TaxPub DTD file path.
        /// </summary>
        public static readonly string TaxPubDtdPath = AppSettings.TaxPubDtdPath;
    }
}
