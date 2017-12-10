﻿namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Coordinates;

    [System.ComponentModel.Description("Tag coordinates.")]
    public class TagCoordinatesCommand : DocumentTaggerCommand<ICoordinatesTagger>, ITagCoordinatesCommand
    {
        public TagCoordinatesCommand(ICoordinatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
