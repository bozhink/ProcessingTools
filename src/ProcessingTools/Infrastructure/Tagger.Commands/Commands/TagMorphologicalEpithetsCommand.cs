namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Tag morphological epithets.")]
    public class TagMorphologicalEpithetsCommand : GenericDocumentTaggerCommand<IMorphologicalEpithetsTagger>, ITagMorphologicalEpithetsCommand
    {
        public TagMorphologicalEpithetsCommand(IMorphologicalEpithetsTagger tagger)
            : base(tagger)
        {
        }
    }
}
