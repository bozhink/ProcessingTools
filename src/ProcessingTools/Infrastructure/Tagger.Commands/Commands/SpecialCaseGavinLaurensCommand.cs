namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Special.Processors.Contracts.Processors;

    public class SpecialCaseGavinLaurensCommand : GenericDocumentParserCommand<IGavinLaurensParser>, ISpecialCaseGavinLaurensCommand
    {
        public SpecialCaseGavinLaurensCommand(IGavinLaurensParser parser)
            : base(parser)
        {
        }
    }
}
