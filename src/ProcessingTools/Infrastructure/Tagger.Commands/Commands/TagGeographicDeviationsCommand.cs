namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Geo;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag geographic deviations.")]
    public class TagGeographicDeviationsCommand : GenericDocumentTaggerCommand<IGeographicDeviationsTagger>, ITagGeographicDeviationsCommand
    {
        public TagGeographicDeviationsCommand(IGeographicDeviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}
