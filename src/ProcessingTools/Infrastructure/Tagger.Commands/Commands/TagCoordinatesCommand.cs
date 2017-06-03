namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Coordinates;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag coordinates.")]
    public class TagCoordinatesCommand : GenericDocumentTaggerCommand<ICoordinatesTagger>, ITagCoordinatesCommand
    {
        public TagCoordinatesCommand(ICoordinatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
