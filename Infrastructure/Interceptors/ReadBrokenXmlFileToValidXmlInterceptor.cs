namespace ProcessingTools.Interceptors
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Ninject.Extensions.Interception;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Files.IO;
    using ProcessingTools.Contracts.Types;

    public class ReadBrokenXmlFileToValidXmlInterceptor : IInterceptor
    {
        private readonly ILogger logger;

        public ReadBrokenXmlFileToValidXmlInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!(invocation.Request.Target is IXmlFileReader) || (invocation.Request.Method.Name != nameof(IXmlFileReader.ReadXml)))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                // Intercept async call
                invocation.Proceed();
                invocation.ReturnValue = Task.FromResult(((Task<XmlDocument>)invocation.ReturnValue).Result);
            }
            catch (AggregateException e)
            {
                var fullNameParameter = invocation.Request.Arguments[0];

                if (e.InnerExceptions.Count == 1 &&
                    e.InnerExceptions.Single() is XmlException)
                {
                    var document = this.RestoreXmlDocument(fullNameParameter);
                    invocation.ReturnValue = Task.FromResult(document);
                    this.logger?.Log(
                        LogType.Info,
                        e.InnerExceptions.Single(),
                        "Input file name '{0}' is not a valid XML document.\nIt will be read as text file and will be wrapped in basic XML tags.\n",
                        fullNameParameter);
                }
                else
                {
                    this.logger?.Log(e, "Input file name: {0}", fullNameParameter);
                    throw e;
                }
            }
        }

        private XmlDocument RestoreXmlDocument(object fullName)
        {
            if (fullName == null)
            {
                throw new ArgumentNullException(nameof(fullName));
            }

            var document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var body = document.CreateElement(ElementNames.BodyElementName);
            foreach (var line in File.ReadLines(fullName.ToString()))
            {
                var paragraph = document.CreateElement(ElementNames.ParagraphElementName);
                paragraph.InnerText = line;
                body.AppendChild(paragraph);
            }

            var documentElement = document.CreateElement(ElementNames.ArticleElementName);
            documentElement.AppendChild(body);

            document.AppendChild(documentElement);

            return document;
        }
    }
}
