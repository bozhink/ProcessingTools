namespace ProcessingTools.MediaType.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MediaTypeDataServiceWindowsRegistryTests
    {
        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            Assert.IsNotNull(service, "Object should not be null.");
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDotKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = ".txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension.TrimStart('.'), type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithUnknownFileExtension_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "unknown-file-extension";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(500)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataServiceWindowsRegistry_WithNullFileExtension_ShouldThowAggregateException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(null).Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithNullFileExtension_ShouldThowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediaTypeDataServiceWindowsRegistry();
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
        public void MediaTypeDataServiceWindowsRegistry_WithEmptyFileExtension_ShouldThowAggregateException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(@"   
                                            ").Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithEmptyFileExtension_ShouldThowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediaTypeDataServiceWindowsRegistry();
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
        public void MediaTypeDataServiceWindowsRegistry_WithDotFileExtension_ShouldThowAggregateException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(".").Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDotFileExtension_ShouldThowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediaTypeDataServiceWindowsRegistry();
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
        public void MediaTypeDataServiceWindowsRegistry_WithDotEmptyFileExtension_ShouldThowAggregateException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(@"  
                        .   .. ").Result;
        }

        [TestMethod]
        [Timeout(500)]
        public void MediaTypeDataServiceWindowsRegistry_WithDotEmptyFileExtension_ShouldThowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var service = new MediaTypeDataServiceWindowsRegistry();
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