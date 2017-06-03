namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Quantities;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag quantities.")]
    public class TagQuantitiesCommand : GenericDocumentTaggerCommand<IQuantitiesTagger>, ITagQuantitiesCommand
    {
        public TagQuantitiesCommand(IQuantitiesTagger tagger)
            : base(tagger)
        {
        }
    }
}
