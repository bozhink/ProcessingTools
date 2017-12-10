namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio;

    [System.ComponentModel.Description("Tag morphological epithets.")]
    public class TagMorphologicalEpithetsCommand : DocumentTaggerCommand<IMorphologicalEpithetsTagger>, ITagMorphologicalEpithetsCommand
    {
        public TagMorphologicalEpithetsCommand(IMorphologicalEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
