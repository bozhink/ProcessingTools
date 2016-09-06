namespace ProcessingTools.Processors
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class DocumentXslProcessor : IDocumentXslProcessor
    {
        private string xslFilePath;

        public string XslFilePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.xslFilePath))
                {
                    throw new NullReferenceException("File path of the XSLT file should not be null or white-space.");
                }

                return this.xslFilePath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.XslFilePath));
                }

                this.xslFilePath = value;
            }
        }

        public Task Process(IDocument context) => Task.Run(() =>
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Xml = context.Xml.ApplyXslTransform(this.XslFilePath);
        });
    }
}
