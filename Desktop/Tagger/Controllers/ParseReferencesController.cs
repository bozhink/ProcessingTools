namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.References;

    [Description("Parse references.")]
    public class ParseReferencesController : GenericXmlContextParserController<IReferencesParser>, IParseReferencesController
    {
        public ParseReferencesController(IReferencesParser parser)
            : base(parser)
        {
        }
    }
}
