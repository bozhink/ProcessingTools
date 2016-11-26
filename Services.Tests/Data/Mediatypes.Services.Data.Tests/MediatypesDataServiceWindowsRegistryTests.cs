namespace ProcessingTools.Mediatypes.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MediatypesDataServiceWindowsRegistryTests
    {
        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var service = new MediatypesDataServiceWindowsRegistry();
            Assert.IsNotNull(service, "Object should not be null.");
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediatypesDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.Mimetype, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.Mimesubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDotKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = ".txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediatypesDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension.TrimStart('.'), type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.Mimetype, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.Mimesubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithUnknownFileExtension_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "unknown-file-extension";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var service = new MediatypesDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.Mimetype, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.Mimesubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(500)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataServiceWindowsRegistry_WithNullFileExtension_ShouldThrowAggregateException()
        {
            var service = new MediatypesDataServiceWindowsRegistry();
            var type = service.GetMediaType(null).Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithNullFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediatypesDataServiceWindowsRegistry();
                var type = service.GetMediaType(null).Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count(), "Number of inner exceptions should be 1.");
                Assert.IsInstanceOfType(
                    e.InnerExceptions.First(),
                    typeof(ArgumentNullException),
                    "Inner exception should be ArgumentNullException.");
            }
        }

        [TestMethod]
        [Timeout(500)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataServiceWindowsRegistry_WithEmptyFileExtension_ShouldThrowAggregateException()
        {
            var service = new MediatypesDataServiceWindowsRegistry();
            var type = service.GetMediaType(@"   
                                            ").Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithEmptyFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediatypesDataServiceWindowsRegistry();
                var type = service.GetMediaType(@"   
                                            ").Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count(), "Number of inner exceptions should be 1.");
                Assert.IsInstanceOfType(
                    e.InnerExceptions.First(),
                    typeof(ArgumentNullException),
                    "Inner exception should be ArgumentNullException.");
            }
        }

        [TestMethod]
        [Timeout(500)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataServiceWindowsRegistry_WithDotFileExtension_ShouldThrowAggregateException()
        {
            var service = new MediatypesDataServiceWindowsRegistry();
            var type = service.GetMediaType(".").Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDotFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediatypesDataServiceWindowsRegistry();
                var type = service.GetMediaType(".");
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count(), "Number of inner exceptions should be 1.");
                Assert.IsInstanceOfType(
                    e.InnerExceptions.First(),
                    typeof(ArgumentNullException),
                    "Inner exception should be ArgumentNullException.");
            }
        }

        [TestMethod]
        [Timeout(500)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataServiceWindowsRegistry_WithDotEmptyFileExtension_ShouldThrowAggregateException()
        {
            var service = new MediatypesDataServiceWindowsRegistry();
            var type = service.GetMediaType(@"  
                        .   .. ").Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDotEmptyFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediatypesDataServiceWindowsRegistry();
                var type = service.GetMediaType(@"  
                        .   .. ").Result;
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(1, e.InnerExceptions.Count(), "Number of inner exceptions should be 1.");
                Assert.IsInstanceOfType(
                    e.InnerExceptions.First(),
                    typeof(ArgumentNullException),
                    "Inner exception should be ArgumentNullException.");
            }
        }
    }
}