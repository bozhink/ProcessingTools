namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Xml.Xsl;

    using Contracts;
    using Extensions;
    using ProcessingTools.Contracts;

    public class CustomXslRunner : Base, IProcessor
    {
        private XslCompiledTransform xslTransform;

        public CustomXslRunner(string xslPath, string xml)
            : base(xml)
        {
            this.xslTransform = new XslCompiledTransform();
            this.XslPath = xslPath;
        }

        public CustomXslRunner(string xslPath, IBase baseObject)
            : base(baseObject)
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

        public void Process()
        {
            this.Xml = this.Xml.ApplyXslTransform(this.xslTransform);
        }
    }
}