namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Geo;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag geo epithets.")]
    public class TagGeoEpithetsCommand : GenericDocumentTaggerCommand<IGeoEpithetsTagger>, ITagGeoEpithetsCommand
    {
        public TagGeoEpithetsCommand(IGeoEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
