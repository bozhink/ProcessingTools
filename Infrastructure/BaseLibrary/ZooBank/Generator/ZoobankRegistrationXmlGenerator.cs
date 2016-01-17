namespace ProcessingTools.BaseLibrary.ZooBank
{
    using System.Threading.Tasks;

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

        public Task Generate()
        {
            return Task.Run(() => this.Xml = this.XmlDocument.ApplyXslTransform(this.Config.ZoobankNlmXslPath));
        }
    }
}