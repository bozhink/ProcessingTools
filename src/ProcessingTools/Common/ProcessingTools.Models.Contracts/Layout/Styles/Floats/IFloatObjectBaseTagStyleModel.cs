// <copyright file="IFloatObjectBaseTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Layout.Styles.Floats
{
    /// <summary>
    /// Float object base tag style model.
    /// </summary>
    public interface IFloatObjectBaseTagStyleModel : IFloatObjectStyleModel, ITagStyleModel
    {
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
    }
}
