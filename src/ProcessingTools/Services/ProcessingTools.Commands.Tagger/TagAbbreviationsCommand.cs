namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Abbreviations;

    [System.ComponentModel.Description("Tag abbreviations.")]
    public class TagAbbreviationsCommand : XmlContextTaggerCommand<IAbbreviationsTagger>, ITagAbbreviationsCommand
    {
        public TagAbbreviationsCommand(IAbbreviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}
