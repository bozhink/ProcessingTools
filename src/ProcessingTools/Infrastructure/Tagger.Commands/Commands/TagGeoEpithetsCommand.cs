namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Geo;

    [System.ComponentModel.Description("Tag geo epithets.")]
    public class TagGeoEpithetsCommand : DocumentTaggerCommand<IGeoEpithetsTagger>, ITagGeoEpithetsCommand
    {
        public TagGeoEpithetsCommand(IGeoEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
