using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProcessingTools.Documents.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTools.Documents.Services.Data.Tests
{
    [TestClass]
    public class XmlPresenterTests
    {
        [TestMethod]
        public void XmlPresenter_WithValidServiceInConstructor_ShouldReturnValidObject()
        {
            var serviceMock = new Mock<IDocumentsDataService>();
            var service = serviceMock.Object;

            var presenter = new XmlPresenter(service);
            Assert.IsNotNull(presenter, "Object should not be null");

            string fieldName = nameof(service);
            var field = presenter.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldValue = field?.GetValue(presenter);
            Assert.IsNotNull(fieldValue, "Service should be initialized.");
            Assert.AreSame(service, fieldValue, "Service should be set correctly.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void XmlPresenter_WithNullServiceInConstructor_ShouldThrowArgumentNullException()
        {
            var presenter = new XmlPresenter(null);
        }

        [TestMethod]
        public void XmlPresenter_WithNullServiceInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            try
            {
                var presenter = new XmlPresenter(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("service", e.ParamName, "ParamName should be 'service'.");
            }
        }
    }
}
