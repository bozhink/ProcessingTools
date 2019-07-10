// <copyright file="ExpandLowerTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;

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
