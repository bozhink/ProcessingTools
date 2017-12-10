namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.References;

    [System.ComponentModel.Description("Parse references.")]
    public class ParseReferencesCommand : XmlContextParserCommand<IReferencesParser>, IParseReferencesCommand
    {
        public ParseReferencesCommand(IReferencesParser parser)
            : base(parser)
        {
        }
    }
}
