namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Geo;

    [System.ComponentModel.Description("Tag geo names.")]
    public class TagGeoNamesCommand : DocumentTaggerCommand<IGeoNamesTagger>, ITagGeoNamesCommand
    {
        public TagGeoNamesCommand(IGeoNamesTagger tagger)
            : base(tagger)
        {
        }
    }
}
