namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Special.Processors.Contracts.Processors;
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
