namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Parse lower taxa.")]
    public class ParseLowerTaxaCommand : GenericXmlContextParserCommand<ILowerTaxaParser>, IParseLowerTaxaCommand
    {
        public ParseLowerTaxaCommand(ILowerTaxaParser parser)
            : base(parser)
        {
        }
    }
}
