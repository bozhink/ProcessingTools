namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;

    [Description("Expand lower taxa.")]
    public class ExpandLowerTaxaCommand : GenericXmlContextParserCommand<IExpander>, IExpandLowerTaxaCommand
    {
        public ExpandLowerTaxaCommand(IExpander parser)
            : base(parser)
        {
        }
    }
}
