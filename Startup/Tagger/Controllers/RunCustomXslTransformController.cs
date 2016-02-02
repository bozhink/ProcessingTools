namespace ProcessingTools.MainProgram.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Custom XSL transform.")]
    public class RunCustomXslTransformController : TaggerControllerFactory, IRunCustomXslTransformController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            int numberOfFileNames = settings.FileNames.Count();

            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The name of the XSLT file should be set.");
            }

            string xslFileName = settings.FileNames.ElementAt(2);

            var processor = new CustomXslRunner(xslFileName, document.OuterXml);

            await processor.Process();

            document.LoadXml(processor.Xml);
        }
    }
}