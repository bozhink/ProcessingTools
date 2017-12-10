namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Quantities;

    [System.ComponentModel.Description("Tag quantities.")]
    public class TagQuantitiesCommand : DocumentTaggerCommand<IQuantitiesTagger>, ITagQuantitiesCommand
    {
        public TagQuantitiesCommand(IQuantitiesTagger tagger)
            : base(tagger)
        {
        }
    }
}
