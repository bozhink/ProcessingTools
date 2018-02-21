namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
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
