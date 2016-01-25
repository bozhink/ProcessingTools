﻿namespace ProcessingTools.MediaType.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MediaType.Data.Models;
    using MediaType.Data.Repositories.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class MediaTypeDataServiceTests
    {
        [TestMethod]
        public void MediaTypeDataService_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            Assert.IsNotNull(service, "Object should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataService_WithNullFileExtension_ShouldThowArgumentNullException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataService_WithEmptyFileExtension_ShouldThowArgumentNullException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(@"
                                            ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataService_WithDotFileExtension_ShouldThowArgumentNullException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(".");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MediaTypeDataService_WithDotEmptyFileExtension_ShouldThowArgumentNullException()
        {
            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(@"
                        .   .. ");
        }

        [TestMethod]
        public void MediaTypeDataService_WithKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => new List<FileExtension>
                {
                    new FileExtension
                    {
                        Name = FileExtension,
                        MimeTypePairs = new List<MimeTypePair>
                        {
                            new MimeTypePair
                            {
                                MimeType = new MimeType
                                {
                                    Name = MimeType
                                },
                                MimeSubtype = new MimeSubtype
                                {
                                    Name = MimeSubtype
                                }
                            }
                        }
                    }
                }
                .AsQueryable());

            // Assert that the mock object works correctly.
            {
                Assert.AreEqual(FileExtension, fileExtensionsRepositoryMock.Object.All().FirstOrDefault().Name, "FileExtension. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeType, fileExtensionsRepositoryMock.Object.All().FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeType.Name, "MimeType. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeSubtype, fileExtensionsRepositoryMock.Object.All().FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeSubtype.Name, "MimeSubtype. Mock object is incorrect. Test is broken.");
            }

            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        public void MediaTypeDataService_WithDotKnownFileExtension_ShouldReturnValidMediaType()
        {
            const string FileExtension = ".txt";
            const string MimeType = "text";
            const string MimeSubtype = "plain";

            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => new List<FileExtension>
                {
                    new FileExtension
                    {
                        Name = FileExtension.TrimStart('.'),
                        MimeTypePairs = new List<MimeTypePair>
                        {
                            new MimeTypePair
                            {
                                MimeType = new MimeType
                                {
                                    Name = MimeType
                                },
                                MimeSubtype = new MimeSubtype
                                {
                                    Name = MimeSubtype
                                }
                            }
                        }
                    }
                }
                .AsQueryable());

            // Assert that the mock object works correctly.
            {
                Assert.AreEqual(FileExtension.TrimStart('.'), fileExtensionsRepositoryMock.Object.All().FirstOrDefault().Name, "FileExtension. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeType, fileExtensionsRepositoryMock.Object.All().FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeType.Name, "MimeType. Mock object is incorrect. Test is broken.");

                Assert.AreEqual(MimeSubtype, fileExtensionsRepositoryMock.Object.All().FirstOrDefault().MimeTypePairs.FirstOrDefault().MimeSubtype.Name, "MimeSubtype. Mock object is incorrect. Test is broken.");
            }

            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).FirstOrDefault();

            Assert.AreEqual(FileExtension.TrimStart('.'), type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        public void MediaTypeDataService_WithEmptyDatabase_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "txt";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => new List<FileExtension>().AsQueryable());

            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }

        [TestMethod]
        public void MediaTypeDataService_WithUnknownFileExtension_ShouldReturnUnknownMediaType()
        {
            const string FileExtension = "unknown-file-extension";
            const string MimeType = "unknown";
            const string MimeSubtype = "unknown";

            var fileExtensionsRepositoryMock = new Mock<IMediaTypesRepository<FileExtension>>();
            fileExtensionsRepositoryMock
                .Setup(f => f.All())
                .Returns(() => new List<FileExtension>()
                {
                    new FileExtension
                    {
                        Name = "txt",
                        MimeTypePairs = new List<MimeTypePair>()
                        {
                            new MimeTypePair
                            {
                                MimeType = new MimeType
                                {
                                    Name = "text"
                                },
                                MimeSubtype = new MimeSubtype
                                {
                                    Name = "plain"
                                }
                            }
                        }
                    }
                }
                .AsQueryable());

            var service = new MediaTypeDataService(fileExtensionsRepositoryMock.Object);
            var type = service.GetMediaType(FileExtension).FirstOrDefault();

            Assert.AreEqual(FileExtension, type.FileExtension, "FileExtension should match.");
            Assert.AreEqual(MimeType, type.MimeType, "MimeType should match.");
            Assert.AreEqual(MimeSubtype, type.MimeSubtype, "MimeSubtype should match.");
        }
    }
}