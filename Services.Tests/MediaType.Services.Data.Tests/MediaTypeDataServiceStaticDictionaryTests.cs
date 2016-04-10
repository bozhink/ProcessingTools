namespace ProcessingTools.MediaType.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Services;

    [TestClass]
    public class MediaTypeDataServiceStaticDictionaryTests
    {
        [TestMethod]
        public void MediaTypeDataServiceStaticDictionary_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var service = new MediaTypeDataServiceStaticDictionary();
            Assert.IsNotNull(service, "Object should not be null.");
        }

        [TestMethod]
        public void MediaTypeDataServiceStaticDictionary_WithKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediaTypeDataServiceStaticDictionary();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        public void MediaTypeDataServiceStaticDictionary_WithDotKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = ".txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var service = new MediaTypeDataServiceStaticDictionary();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension.TrimStart('.'), type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        public void MediaTypeDataServiceStaticDictionary_WithUnknownFileExtension_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "unknown-file-extension";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var service = new MediaTypeDataServiceStaticDictionary();
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithNullFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceStaticDictionary();
            var type = service.GetMediaType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithEmptyFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceStaticDictionary();
            var type = service.GetMediaType(@"   
                                            ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithDotFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceStaticDictionary();
            var type = service.GetMediaType(".");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataServiceWindowsRegistry_WithDotEmptyFileExtension_ShouldThowArgumentNullException()
        {
            var service = new MediaTypeDataServiceStaticDictionary();
            var type = service.GetMediaType(@"  
                        .   .. ");
        }
    }
}