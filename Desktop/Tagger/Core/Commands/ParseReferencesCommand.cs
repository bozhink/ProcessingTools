namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.References;

    [Description("Parse references.")]
    public class ParseReferencesCommand : GenericXmlContextParserCommand<IReferencesParser>, IParseReferencesCommand
    {
        public ParseReferencesCommand(IReferencesParser parser)
            : base(parser)
        {
        }
    }
}
