namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Geo;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag geo names.")]
    public class TagGeoNamesCommand : GenericDocumentTaggerCommand<IGeoNamesTagger>, ITagGeoNamesCommand
    {
        public TagGeoNamesCommand(IGeoNamesTagger tagger)
            : base(tagger)
        {
        }
    }
}
