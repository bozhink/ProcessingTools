namespace ProcessingTools.Harvesters
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Infrastructure.Transform;
    
    // TODO
    public class AbbreviationsHarvester : GenericHarvesterFactory<IAbbreviationModel>, IAbbreviationsHarvester
    {
        protected override Task<IQueryable<IAbbreviationModel>> Run(XmlDocument document)
        {
            string xqueryFilePath = ConfigurationManager.AppSettings["AbbreviationsXQueryFilePath"];

            var transformer = new XQueryTransform();
            transformer.Load(new FileStream(xqueryFilePath, FileMode.Open));

            var result = transformer.Evaluate(document.DocumentElement);

            Console.WriteLine(result.OuterXml);

            // TODO
            return Task.Run(() =>
            {
                return new HashSet<IAbbreviationModel>().AsQueryable();
            });
        }
    }
}
