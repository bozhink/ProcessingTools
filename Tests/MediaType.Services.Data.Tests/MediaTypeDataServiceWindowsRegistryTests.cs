namespace ProcessingTools.MediaType.Services.Data.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MediaTypeDataServiceWindowsRegistryTests
    {
        [TestMethod]
        public void MediaTypeDataServiceWindowsRegistry_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            Assert.IsNotNull(service, "Object schould not be null.");
        }

        [TestMethod]
        public void MediaTypeDataServiceWindowsRegistry_WithKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension schould match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType schould match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype schould match.");
        }

        [TestMethod]
        public void MediaTypeDataServiceWindowsRegistry_WithDotKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = ".txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).FirstOrDefault();

            Assert.AreEqual(FileExtension.TrimStart('.'), type.FileExtension, "FileExtension schould match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType schould match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype schould match.");
        }

        [TestMethod]
        public void MediaTypeDataServiceWindowsRegistry_WithUnknownFileExtension_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "unknown-file-extension";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(FileExtension).FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension schould match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType schould match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype schould match.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithNullFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithEmptyFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(@"   
                                            ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithDotFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(".");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithDotEmptyFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceWindowsRegistry();
            var type = service.GetMediaType(@"  
                        .   .. ");
        }
    }
}