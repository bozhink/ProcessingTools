namespace ProcessingTools.Xml.Providers
{
    using System;
    using System.IO;

    using Contracts;
    using Contracts.Providers;
    using Factories;

    public class ModifiableXslTransformProvider : XslTransformAbstractProvider, IModifiableXslTransformProvider
    {
        private string xslFilePath;

        public ModifiableXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        public string XslFilePath
        {
            get
            {
                return this.xslFilePath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.XslFilePath));
                }

                if (!File.Exists(value))
                {
                    throw new FileNotFoundException(message: "XSLT file is not found", fileName: value);
                }

                this.xslFilePath = value;
            }
        }

        protected override string XslFileName => this.XslFilePath;
    }
}
