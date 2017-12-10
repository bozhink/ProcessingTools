namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Expand lower taxa.")]
    public class ExpandLowerTaxaCommand : GenericXmlContextParserCommand<IExpander>, IExpandLowerTaxaCommand
    {
        public ExpandLowerTaxaCommand(IExpander parser)
            : base(parser)
        {
        }
    }
}
