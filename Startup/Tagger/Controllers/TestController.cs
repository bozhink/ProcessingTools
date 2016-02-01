﻿namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Test.")]
    public class TestController : TaggerControllerFactory, ITestController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() =>
            {
                var test = new Test(document.OuterXml);

                test.WrapEmptySuperscriptsInFootnoteXrefTag();

                document.LoadXml(test.Xml);
            });
        }
    }
}
