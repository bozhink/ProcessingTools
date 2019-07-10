﻿// <copyright file="InitialFormatCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Layout;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

    /// <summary>
    /// Initial format command.
    /// </summary>
    [System.ComponentModel.Description("Initial format.")]
    public class InitialFormatCommand : DocumentFormatterCommand<IDocumentInitialFormatter>, IInitialFormatCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitialFormatCommand"/> class.
        /// </summary>
        /// <param name="formatter">Instance of <see cref="IDocumentInitialFormatter"/>.</param>
        public InitialFormatCommand(IDocumentInitialFormatter formatter)
            : base(formatter)
        {
        }
    }
}
