// <copyright file="IJournalBaseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Layout.Styles.Journals
{
    using System.Collections.Generic;

    /// <summary>
    /// Journal base style model.
    /// </summary>
    public interface IJournalBaseStyleModel : IStyleModel
    {
        /// <summary>
        /// Gets the ID-s of the referenced float object parse styles.
        /// </summary>
        IList<string> FloatObjectParseStyleIds { get; }

        /// <summary>
        /// Gets the ID-s of the referenced float object tag styles.
        /// </summary>
        IList<string> FloatObjectTagStyleIds { get; }

        /// <summary>
        /// Gets the ID-s of the referenced reference parse styles.
        /// </summary>
        IList<string> ReferenceParseStyleIds { get; }

        /// <summary>
        /// Gets the ID-s of the referenced reference tag styles.
        /// </summary>
        IList<string> ReferenceTagStyleIds { get; }
    }
}
