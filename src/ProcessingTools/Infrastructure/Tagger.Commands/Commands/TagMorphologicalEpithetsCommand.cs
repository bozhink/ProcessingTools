namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio;

    [System.ComponentModel.Description("Tag morphological epithets.")]
    public class TagMorphologicalEpithetsCommand : DocumentTaggerCommand<IMorphologicalEpithetsTagger>, ITagMorphologicalEpithetsCommand
    {
        public TagMorphologicalEpithetsCommand(IMorphologicalEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
