using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessingTools;
using ProcessingTools.Base;


namespace BaseObjectTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod1()
        {
            XPathProvider xpathProvider = new ProcessingTools.Base.XPathProvider(new Config());
        }
    }
}
