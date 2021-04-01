// <copyright file="FormatTreatmentsCommand.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

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
