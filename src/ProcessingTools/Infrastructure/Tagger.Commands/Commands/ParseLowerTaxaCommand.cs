namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse lower taxa.")]
    public class ParseLowerTaxaCommand : XmlContextParserCommand<ILowerTaxaParser>, IParseLowerTaxaCommand
    {
        public ParseLowerTaxaCommand(ILowerTaxaParser parser)
            : base(parser)
        {
        }
    }
}
