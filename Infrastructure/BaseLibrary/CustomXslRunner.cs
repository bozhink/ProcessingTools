namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Threading.Tasks;

    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;
    using ProcessingTools.Xml.Extensions;

    public class CustomXslRunner : TaxPubDocument, IProcessor
    {
        private string xslPath;

        public CustomXslRunner(string xslPath, string xml)
            : base(xml)
        {
            this.XslPath = xslPath;
        }

        private string XslPath
        {
            get
            {
                return this.xslPath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.XslPath));
                }

                this.xslPath = value;
            }
        }

        public Task Process()
        {
            return Task.Run(() => this.Xml = this.Xml.ApplyXslTransform(this.XslPath));
        }
    }
}