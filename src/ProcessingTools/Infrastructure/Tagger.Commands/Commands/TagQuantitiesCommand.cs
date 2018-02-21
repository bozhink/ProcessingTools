namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Quantities;

    [System.ComponentModel.Description("Tag quantities.")]
    public class TagQuantitiesCommand : DocumentTaggerCommand<IQuantitiesTagger>, ITagQuantitiesCommand
    {
        public TagQuantitiesCommand(IQuantitiesTagger tagger)
            : base(tagger)
        {
        }
    }
}
