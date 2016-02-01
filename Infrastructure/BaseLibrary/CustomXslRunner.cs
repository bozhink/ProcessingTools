namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Threading.Tasks;
    using System.Xml.Xsl;

    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Extensions;

    public class CustomXslRunner : ConfigurableDocument, IProcessor
    {
        private XslCompiledTransform xslTransform;

        public CustomXslRunner(string xslPath, string xml)
            : base(xml)
        {
            this.xslTransform = new XslCompiledTransform();
            this.XslPath = xslPath;
        }

        private string XslPath
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("value");
                }

                this.xslTransform.Load(value);
            }
        }

        public Task Process()
        {
            return Task.Run(() => this.Xml = this.Xml.ApplyXslTransform(this.xslTransform));
        }
    }
}