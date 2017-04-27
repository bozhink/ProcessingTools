namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Abbreviations;

    [Description("Tag abbreviations.")]
    public class TagAbbreviationsCommand : GenericXmlContextTaggerCommand<IAbbreviationsTagger>, ITagAbbreviationsCommand
    {
        public TagAbbreviationsCommand(IAbbreviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}
