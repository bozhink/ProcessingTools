namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Floats;

    [System.ComponentModel.Description("Tag floats.")]
    public class TagFloatsCommand : XmlContextTaggerCommand<IFloatsTagger>, ITagFloatsCommand
    {
        public TagFloatsCommand(IFloatsTagger tagger)
            : base(tagger)
        {
        }
    }
}
