// <copyright file="FormatTreatmentsCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Format treatments command.
    /// </summary>
    [System.ComponentModel.Description("Format treatments.")]
    public class FormatTreatmentsCommand : DocumentFormatterCommand<ITreatmentFormatter>, IFormatTreatmentsCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatTreatmentsCommand"/> class.
        /// </summary>
        /// <param name="formatter">Instance of <see cref="ITreatmentFormatter"/>.</param>
        public FormatTreatmentsCommand(ITreatmentFormatter formatter)
            : base(formatter)
        {
        }
    }
}
