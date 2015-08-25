namespace ProcessingTools.Base.ZooBank
{
    public class ZooBank : TaggerBase
    {

        public ZooBank(string xml)
            : base(xml)
        {
        }

        public ZooBank(Config config, string xml)
            : base(config, xml)
        {
        }

        public ZooBank(TaggerBase baseObject)
            : base(baseObject)
        {
        }

        public void GenerateZooBankNlm()
        {
            this.Xml = this.XmlDocument.ApplyXslTransform(this.Config.zoobankNlmXslPath);
        }
    }
}
