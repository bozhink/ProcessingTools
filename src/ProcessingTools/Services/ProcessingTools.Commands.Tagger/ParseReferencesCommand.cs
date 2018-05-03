namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.References;

    [System.ComponentModel.Description("Parse references.")]
    public class ParseReferencesCommand : XmlContextParserCommand<IReferencesParser>, IParseReferencesCommand
    {
        public ParseReferencesCommand(IReferencesParser parser)
            : base(parser)
        {
        }
    }
}
