namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Geo;

    [System.ComponentModel.Description("Tag altitudes.")]
    public class TagAltitudesCommand : DocumentTaggerCommand<IAltitudesTagger>, ITagAltitudesCommand
    {
        public TagAltitudesCommand(IAltitudesTagger tagger)
            : base(tagger)
        {
        }
    }
}
