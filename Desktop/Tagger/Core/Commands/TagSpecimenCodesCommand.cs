namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio.Codes;

    [Description("Tag specimen codes.")]
    public class TagSpecimenCodesCommand : GenericDocumentTaggerCommand<ISpecimenCodesByPatternTagger>, ITagSpecimenCodesCommand
    {
        public TagSpecimenCodesCommand(ISpecimenCodesByPatternTagger tagger)
            : base(tagger)
        {
        }
    }
}
