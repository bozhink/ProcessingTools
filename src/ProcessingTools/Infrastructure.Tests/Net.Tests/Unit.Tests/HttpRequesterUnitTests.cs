namespace ProcessingTools.Net.Tests.Unit.Tests
{
    using NUnit.Framework;
    using ProcessingTools.Contracts;
    using ProcessingTools.Net;

    [TestFixture]
    public class HttpRequesterUnitTests
    {
        [Test]
        public void HttpRequester_ParameterlessConstructor_ShouldReturnValidObject()
        {
            var requester = new HttpRequester();
            Assert.IsNotNull(requester, "Connector should be a valid object.");
            Assert.IsInstanceOf<IHttpRequester>(requester, "Connector should be a instance of {0}.", nameof(IHttpRequester));
        }
    }
}
