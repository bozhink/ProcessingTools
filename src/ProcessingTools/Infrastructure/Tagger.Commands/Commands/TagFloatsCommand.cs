namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Floats;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag floats.")]
    public class TagFloatsCommand : GenericXmlContextTaggerCommand<IFloatsTagger>, ITagFloatsCommand
    {
        public TagFloatsCommand(IFloatsTagger tagger)
            : base(tagger)
        {
        }
    }
}
