namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
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
