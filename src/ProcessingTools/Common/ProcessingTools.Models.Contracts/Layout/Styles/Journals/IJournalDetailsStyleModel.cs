// <copyright file="IJournalDetailsStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Layout.Styles.Journals
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;

    /// <summary>
    /// Journal details style model.
    /// </summary>
    public interface IJournalDetailsStyleModel : IJournalStyleModel
    {
        /// <summary>
        /// Gets the referenced float object parse styles.
        /// </summary>
        IEnumerable<IFloatObjectDetailsParseStyleModel> FloatObjectParseStyles { get; }

        /// <summary>
        /// Gets the referenced float object tag styles.
        /// </summary>
        IEnumerable<IFloatObjectDetailsTagStyleModel> FloatObjectTagStyles { get; }

        /// <summary>
        /// Gets the referenced reference parse styles.
        /// </summary>
        IEnumerable<IReferenceDetailsParseStyleModel> ReferenceParseStyles { get; }

        /// <summary>
        /// Gets the referenced reference tag styles.
        /// </summary>
        IEnumerable<IReferenceDetailsTagStyleModel> ReferenceTagStyles { get; }
    }
}
