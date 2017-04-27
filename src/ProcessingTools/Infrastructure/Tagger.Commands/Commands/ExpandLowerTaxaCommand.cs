namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Expand lower taxa.")]
    public class ExpandLowerTaxaCommand : GenericXmlContextParserCommand<IExpander>, IExpandLowerTaxaCommand
    {
        public ExpandLowerTaxaCommand(IExpander parser)
            : base(parser)
        {
        }
    }
}
