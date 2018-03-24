namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Geo;

    [System.ComponentModel.Description("Tag geographic deviations.")]
    public class TagGeographicDeviationsCommand : DocumentTaggerCommand<IGeographicDeviationsTagger>, ITagGeographicDeviationsCommand
    {
        public TagGeographicDeviationsCommand(IGeographicDeviationsTagger tagger)
            : base(tagger)
        {
        }
    }
}
