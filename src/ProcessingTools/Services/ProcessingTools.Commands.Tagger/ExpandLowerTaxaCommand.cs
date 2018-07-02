// <copyright file="ExpandLowerTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Expand lower taxa command.
    /// </summary>
    [System.ComponentModel.Description("Expand lower taxa.")]
    public class ExpandLowerTaxaCommand : XmlContextParserCommand<IExpander>, IExpandLowerTaxaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandLowerTaxaCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IExpander"/>.</param>
        public ExpandLowerTaxaCommand(IExpander parser)
            : base(parser)
        {
        }
    }
}
