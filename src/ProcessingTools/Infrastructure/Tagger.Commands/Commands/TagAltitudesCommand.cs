namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Processors.Processors.Geo;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag altitudes.")]
    public class TagAltitudesCommand : GenericDocumentTaggerCommand<IAltitudesTagger>, ITagAltitudesCommand
    {
        public TagAltitudesCommand(IAltitudesTagger tagger)
            : base(tagger)
        {
        }
    }
}
