namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;

    [Description("Parse lower taxa.")]
    public class ParseLowerTaxaController : GenericXmlContextParserController<ILowerTaxaParser>, IParseLowerTaxaController
    {
        public ParseLowerTaxaController(ILowerTaxaParser parser)
            : base(parser)
        {
        }
    }
}
