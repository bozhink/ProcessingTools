using System.Text.RegularExpressions;

namespace Base.ZooBank
{
    public class ZooBank : Base
    {
        public ZooBank()
            : base()
        {
        }

        public ZooBank(string xml)
            : base(xml)
        {
        }

        public ZooBank(Config config)
            : base(config)
        {
        }

        public ZooBank(Config config, string xml)
            : base(config, xml)
        {
        }

        public ZooBank(Base baseObject)
            : base(baseObject)
        {
        }

        public void GenerateZooBankNlm()
        {
            this.xml = XsltOnString.ApplyTransform(this.Config.zoobankNlmXslPath, this.xml);
            this.xml = Regex.Replace(this.xml, @"(?<=<\?xml version=""1\.0"" encoding=""utf\-8""\?>)", "<!DOCTYPE article PUBLIC \"-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN\" \"tax-treatment-NS0.dtd\">");
        }
    }
}
