namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Parse lower taxa.")]
    public class ParseLowerTaxaCommand : GenericXmlContextParserCommand<ILowerTaxaParser>, IParseLowerTaxaCommand
    {
        public ParseLowerTaxaCommand(ILowerTaxaParser parser)
            : base(parser)
        {
        }
    }
}
