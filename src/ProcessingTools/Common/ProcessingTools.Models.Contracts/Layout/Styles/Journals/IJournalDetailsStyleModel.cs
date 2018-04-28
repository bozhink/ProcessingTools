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
        /// Gets the reference float object parse styles.
        /// </summary>
        IList<IFloatObjectDetailsParseStyleModel> FloatObjectParseStyles { get; }

        /// <summary>
        /// Gets the reference float object tag styles.
        /// </summary>
        IList<IFloatObjectDetailsTagStyleModel> FloatObjectTagStyles { get; }

        /// <summary>
        /// Gets the reference reference parse styles.
        /// </summary>
        IList<IReferenceDetailsParseStyleModel> ReferenceParseStyles { get; }

        /// <summary>
        /// Gets the reference reference tag styles.
        /// </summary>
        IList<IReferenceDetailsTagStyleModel> ReferenceTagStyles { get; }
    }
}
