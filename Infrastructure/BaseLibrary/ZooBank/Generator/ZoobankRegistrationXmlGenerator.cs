namespace ProcessingTools.BaseLibrary.ZooBank
{
    using Configurator;
    using Contracts;
    using Extensions;
    using ProcessingTools.Contracts;

    public class ZoobankRegistrationXmlGenerator : Base, IGenerator
    {
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