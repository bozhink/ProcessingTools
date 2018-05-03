namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Processors.Contracts.Geo;

    [System.ComponentModel.Description("Tag geo epithets.")]
    public class TagGeoEpithetsCommand : DocumentTaggerCommand<IGeoEpithetsTagger>, ITagGeoEpithetsCommand
    {
        public TagGeoEpithetsCommand(IGeoEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
