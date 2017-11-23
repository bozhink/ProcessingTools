namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Processors.Contracts.Special;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    public class SpecialCaseGavinLaurensCommand : GenericDocumentParserCommand<IGavinLaurensParser>, ISpecialCaseGavinLaurensCommand
    {
        public SpecialCaseGavinLaurensCommand(IGavinLaurensParser parser)
            : base(parser)
        {
        }
    }
}
