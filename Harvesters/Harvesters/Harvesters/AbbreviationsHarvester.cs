namespace ProcessingTools.Harvesters
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Infrastructure.Extensions;
    using ProcessingTools.Infrastructure.Transform;

    public class AbbreviationsHarvester : GenericHarvesterFactory<IAbbreviationModel>, IAbbreviationsHarvester
    {
        private const string AbbreviationsXQueryFilePath = "AbbreviationsXQueryFilePath";

        private string abbreviationsXQueryFileName;

        public AbbreviationsHarvester()
        {
            this.abbreviationsXQueryFileName = ConfigurationManager.AppSettings[AbbreviationsXQueryFilePath];
        }

        protected override Task<IQueryable<IAbbreviationModel>> Run(XmlDocument document)
        {
            return Task.Run(() =>
               {
                   var transformer = new XQueryTransform();
                   transformer.Load(new FileStream(this.abbreviationsXQueryFileName, FileMode.Open));

                   var result = transformer.Evaluate(document.DocumentElement);
                   var items = result.Deserialize<AbbreviationsModel>();
                   return items.Abbreviations.AsQueryable<IAbbreviationModel>();
               });
        }
    }
}
