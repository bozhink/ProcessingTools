// <copyright file="IDocumentStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Layout.Styles
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Document style model.
    /// </summary>
    public interface IDocumentStyleModel
    {
        /// <summary>
        /// Gets the journal style of the document.
        /// </summary>
        IJournalStyleModel JournalStyle { get; }

        /// <summary>
        /// Gets the list of the float object tag styles.
        /// </summary>
        IList<IFloatObjectTagStyleModel> FloatObjectTagStyles { get; }

        /// <summary>
        /// Gets the list of the float object parse styles.
        /// </summary>
        IList<IFloatObjectParseStyleModel> FloatObjectParseStyles { get; }

        /// <summary>
        /// Gets the list of the reference tag styles.
        /// </summary>
        IList<IReferenceTagStyleModel> ReferenceTagStyles { get; }

        /// <summary>
        /// Gets the list of the reference parse styles.
        /// </summary>
        IList<IReferenceParseStyleModel> ReferenceParseStyles { get; }
    }
}
