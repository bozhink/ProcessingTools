namespace ProcessingTools.Harvesters.Abbreviations
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Abstracts;
    using Contracts.Abbreviations;
    using Models.Abbreviations;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Extensions;

    public class AbbreviationsHarvester : AbstractGenericQueryableXmlHarvester<IAbbreviationModel>, IAbbreviationsHarvester
    {
        private const string AbbreviationsXQueryFilePathKey = "AbbreviationsXQueryFilePath";

        private string abbreviationsXQueryFileName;

        public AbbreviationsHarvester(IXmlContextWrapperProvider contextWrapperProvider)
            : base(contextWrapperProvider)
        {
            // TODO: ConfigurationManager
            this.abbreviationsXQueryFileName = ConfigurationManager.AppSettings[AbbreviationsXQueryFilePathKey];
        }

        protected override async Task<IQueryable<IAbbreviationModel>> Run(XmlDocument document)
        {
            var items = await document.DeserializeXQueryTransformOutput<AbbreviationsXmlModel>(this.abbreviationsXQueryFileName);

            if (items?.Abbreviations == null)
            {
                return null;
            }

            var result = new HashSet<IAbbreviationModel>(items?.Abbreviations);

            return result.AsQueryable();
        }
    }
}
