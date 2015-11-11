namespace ProcessingTools.DocumentProvider.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaxPubXmlDocumentTests
    {
        [TestMethod]
        public void TaxPubXmlDocument_TestMethod1()
        {
            var xml = new TaxPubXmlDocument("<root/>");
            Console.WriteLine(xml.XmlDocument.OuterXml);

            var nm = TaxPubXmlDocument.NamespceManager();
            Console.WriteLine(nm.LookupNamespace("tp"));
        }
    }
}
