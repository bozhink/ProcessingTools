namespace ProcessingTools.BaseLibrary.ZooBank
{
    using Configurator;
    using Globals.Extensions;

    public class ZoobankRegistrationXmlGenerator : Base, IBaseGenerator
    {
        public ZoobankRegistrationXmlGenerator(string xml)
            : base(xml)
        {
        }

        public ZoobankRegistrationXmlGenerator(Config config, string xml)
            : base(config, xml)
        {
        }

        public ZoobankRegistrationXmlGenerator(IBase baseObject)
            : base(baseObject)
        {
        }

        public void Generate()
        {
            this.Xml = this.XmlDocument.ApplyXslTransform(this.Config.ZoobankNlmXslPath);
        }
    }
}
