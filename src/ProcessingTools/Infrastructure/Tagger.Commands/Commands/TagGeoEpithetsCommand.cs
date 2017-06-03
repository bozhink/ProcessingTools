namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Geo;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
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
