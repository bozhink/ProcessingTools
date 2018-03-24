namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Expand lower taxa.")]
    public class ExpandLowerTaxaCommand : XmlContextParserCommand<IExpander>, IExpandLowerTaxaCommand
    {
        public ExpandLowerTaxaCommand(IExpander parser)
            : base(parser)
        {
        }
    }
}
