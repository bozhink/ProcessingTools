namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Floats;

    [Description("Tag floats.")]
    public class TagFloatsCommand : GenericXmlContextTaggerCommand<IFloatsTagger>, ITagFloatsCommand
    {
        public TagFloatsCommand(IFloatsTagger tagger)
            : base(tagger)
        {
        }
    }
}
