namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Special;

    public class SpecialCaseGavinLaurensCommand : DocumentParserCommand<IGavinLaurensParser>, ISpecialCaseGavinLaurensCommand
    {
        public SpecialCaseGavinLaurensCommand(IGavinLaurensParser parser)
            : base(parser)
        {
        }
    }
}
