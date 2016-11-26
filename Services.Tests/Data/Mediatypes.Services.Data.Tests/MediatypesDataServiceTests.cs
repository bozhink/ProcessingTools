namespace ProcessingTools.Mediatypes.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts.Repositories;
    using ProcessingTools.Mediatypes.Data.Entity.Models;

    [TestClass]
    public class MediatypesDataServiceTests
    {
        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            Assert.IsNotNull(service, "Object should not be null.");
        }

        [TestMethod]
        [Timeout(5000)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataService_WithNullFileExtension_ShouldThrowAggregateException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(null).Result;
        }

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithNullFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
                var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
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
        [Timeout(5000)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataService_WithEmptyFileExtension_ShouldThrowArgumentNullException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(@"
                                            ").Result;
        }

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithEmptyFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
                var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
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
        [Timeout(5000)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataService_WithDotFileExtension_ShouldThrowAggregateException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(".").Result;
        }

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithDotFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
                var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
                var type = service.GetMediaType(".").Result;
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
        [Timeout(5000)]
        [ExpectedException(typeof(AggregateException))]
        public void MediaTypeDataService_WithDotEmptyFileExtension_ShouldThrowAggregateException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(@"
                        .   .. ").Result;
        }

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithDotEmptyFileExtension_ShouldThrowAggregateExceptionWithInternalArgumentNullException()
        {
            try
            {
                var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
                var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
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

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => Task.FromResult(new List<FileExtension>
                {
                    new FileExtension
                    {
                        Name = FileExtension,
                        MimeTypePairs = new List<MimetypePair>
                        {
                            new MimetypePair
                            {
                                MimeType = new Mimetype
                                {
                                    Name = MimeType
                                },
                                MimeSubtype = new Mimesubtype
                                {
                                    Name = MimeSubtype
                                }
                            }
                        }
                    }
                }
                .AsQueryable()));

            // Assert that the mock object works correctly.
            {
                Assert.AreEqual(FileExtension, fileExtensionsRepositoryMock.Object.All().Result.FirstOrDefault().Name, "FileExtension. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeType, fileExtensionsRepositoryMock.Object.All().Result.FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeType.Name, "MimeType. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeSubtype, fileExtensionsRepositoryMock.Object.All().Result.FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeSubtype.Name, "MimeSubtype. Mock object is incorrect. Test is broken.");
            }

            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.Mimetype, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.Mimesubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithDotKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = ".txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => Task.FromResult(new List<FileExtension>
                {
                    new FileExtension
                    {
                        Name = FileExtension.TrimStart('.'),
                        MimeTypePairs = new List<MimetypePair>
                        {
                            new MimetypePair
                            {
                                MimeType = new Mimetype
                                {
                                    Name = MimeType
                                },
                                MimeSubtype = new Mimesubtype
                                {
                                    Name = MimeSubtype
                                }
                            }
                        }
                    }
                }
                .AsQueryable()));

            // Assert that the mock object works correctly.
            {
                Assert.AreEqual(FileExtension.TrimStart('.'), fileExtensionsRepositoryMock.Object.All().Result.FirstOrDefault().Name, "FileExtension. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeType, fileExtensionsRepositoryMock.Object.All().Result.FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeType.Name, "MimeType. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeSubtype, fileExtensionsRepositoryMock.Object.All().Result.FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeSubtype.Name, "MimeSubtype. Mock object is incorrect. Test is broken.");
            }

            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension.TrimStart('.'), type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.Mimetype, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.Mimesubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithEmptyDatabase_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => Task.FromResult(new List<FileExtension>().AsQueryable()));

            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.Mimetype, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.Mimesubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        [Timeout(5000)]
        public void MediaTypeDataService_WithUnknownFileExtension_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "unknown-file-extension";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var fileExtensionsRepositoryMock = new Mock<IMediatypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => Task.FromResult(new List<FileExtension>()
                {
                    new FileExtension
                    {
                        Name = "txt",
                        MimeTypePairs = new List<MimetypePair>()
                        {
                            new MimetypePair
                            {
                                MimeType = new Mimetype
                                {
                                    Name = "text"
                                },
                                MimeSubtype = new Mimesubtype
                                {
                                    Name = "plain"
                                }
                            }
                        }
                    }
                }
                .AsQueryable()));

            var service = new MediatypesDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).Result.FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.Mimetype, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.Mimesubtype, "MimeSubtype should match.");
        }
    }
}
