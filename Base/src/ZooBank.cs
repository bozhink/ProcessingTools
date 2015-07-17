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

        public void GenerateZooBankNlm()
        {
            this.xml = XsltOnString.ApplyTransform(this.config.zoobankNlmXslPath, this.xml);
            this.xml = Regex.Replace(this.xml, @"(?<=<\?xml version=""1\.0"" encoding=""utf\-8""\?>)", "<!DOCTYPE article PUBLIC \"-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN\" \"tax-treatment-NS0.dtd\">");
        }
    }
}
