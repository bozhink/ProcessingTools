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
			xml = XsltOnString.ApplyTransform(config.zoobankNlmXslPath, xml);
			xml = Regex.Replace(xml, @"(?<=<\?xml version=""1\.0"" encoding=""utf\-8""\?>)", "<!DOCTYPE article PUBLIC \"-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN\" \"tax-treatment-NS0.dtd\">");
		}
	}
}
