// <copyright file="ParseLowerTaxaCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Parse lower taxa command.
    /// </summary>
    [System.ComponentModel.Description("Parse lower taxa.")]
    public class ParseLowerTaxaCommand : XmlContextParserCommand<ILowerTaxaParser>, IParseLowerTaxaCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseLowerTaxaCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="ILowerTaxaParser"/>.</param>
        public ParseLowerTaxaCommand(ILowerTaxaParser parser)
            : base(parser)
        {
        }
    }
}
