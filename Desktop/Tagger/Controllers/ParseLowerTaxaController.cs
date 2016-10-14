namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Contracts;

    [Description("Parse lower taxa.")]
    public class ParseLowerTaxaController : TaggerControllerFactory, IParseLowerTaxaController
    {
        private readonly ILowerTaxaParser parser;

        public ParseLowerTaxaController(IDocumentFactory documentFactory, ILowerTaxaParser parser)
            : base(documentFactory)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.parser.Parse(document.XmlDocument.DocumentElement);
        }
    }
}
