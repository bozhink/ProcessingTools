namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;

    [Description("Expand lower taxa.")]
    public class ExpandLowerTaxaController : GenericXmlContextParserController<IExpander>, IExpandLowerTaxaController
    {
        public ExpandLowerTaxaController(IExpander parser)
            : base(parser)
        {
        }
    }
}
