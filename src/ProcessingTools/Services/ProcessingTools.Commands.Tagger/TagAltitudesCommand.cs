namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
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
