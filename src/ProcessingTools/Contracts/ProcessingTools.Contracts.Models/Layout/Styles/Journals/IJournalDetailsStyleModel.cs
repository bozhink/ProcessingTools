// <copyright file="IJournalDetailsStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Layout.Styles.Journals
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

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
