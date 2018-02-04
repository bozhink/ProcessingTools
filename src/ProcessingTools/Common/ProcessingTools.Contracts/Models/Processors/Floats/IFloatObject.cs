// <copyright file="IFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Processors.Floats
{
    using ProcessingTools.Enumerations.Nlm;

    /// <summary>
    /// Floating object.
    /// </summary>
    public interface IFloatObject
    {
        /// <summary>
        /// Gets description of the float object.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the reference type of the floating object according to NLM schema.
        /// </summary>
        ReferenceType FloatReferenceType { get; }

        /// <summary>
        /// Gets the name in the label of the floating object.
        /// </summary>
        string FloatTypeNameInLabel { get; }

        /// <summary>
        /// Gets the regex pattern to match citations of the floating object in text.
        /// </summary>
        string MatchCitationPattern { get; }

        /// <summary>
        /// Gets the value of the xref/@ref-type which will be used only during the tagging process.
        /// </summary>
        string InternalReferenceType { get; }

        /// <summary>
        /// Gets the value of the xref/@ref-type which will be used in the final XML.
        /// </summary>
        string ResultantReferenceType { get; }

        /// <summary>
        /// Gets the XPath for selection of the XML objects which provide information about the floating object.
        /// </summary>
        string FloatObjectXPath { get; }
    }
}
