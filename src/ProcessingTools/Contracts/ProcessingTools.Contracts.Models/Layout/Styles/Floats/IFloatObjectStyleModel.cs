// <copyright file="IFloatObjectStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Layout.Styles.Floats
{
    using ProcessingTools.Common.Enumerations.Nlm;

    /// <summary>
    /// Float object style model.
    /// </summary>
    public interface IFloatObjectStyleModel : IStyleModel
    {
        /// <summary>
        /// Gets the reference type of the floating object according to NLM schema.
        /// </summary>
        ReferenceType FloatReferenceType { get; }

        /// <summary>
        /// Gets the XPath for selection of the XML objects which provide information about the floating object.
        /// </summary>
        string FloatObjectXPath { get; }
    }
}
