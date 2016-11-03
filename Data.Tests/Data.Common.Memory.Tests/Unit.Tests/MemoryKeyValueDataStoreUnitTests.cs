namespace ProcessingTools.Data.Common.Memory.Tests.Unit.Tests
{
    using System;
    using Common;
    using Models;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Data.Common.Memory;

    [TestFixture(Category = "Unit", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>))]
    public class MemoryKeyValueDataStoreUnitTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore AddOrUpdate with all null parameters should throw ArgumentNullException.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_AddOrUpdateAllNullParameters_ShouldThrowArgumentNullException()
        {
            // Arrange
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                db.AddOrUpdate(null, null, null);
            });
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore AddOrUpdate with null addValueFactory should throw ArgumentNullException with correct ParamName.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_AddOrUpdateWithNullAddValueFactory_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var keyMock = new Mock<IKeyModel>();
            keyMock
                .SetupGet(k => k.Id)
                .Returns(1);

            var valueMock = new Mock<IValueModel>();
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                db.AddOrUpdate(keyMock.Object, null, (k, v) => valueMock.Object);
            });

            Assert.AreEqual(Constants.AddValueFactoryParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore AddOrUpdate with null addValueFactory and null updateValueFactory should throw ArgumentNullException.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_AddOrUpdateWithNullAddValueFactoryAndNullUpdateValueFactory_ShouldThrowArgumentNullException()
        {
            // Arrange
            var keyMock = new Mock<IKeyModel>();
            keyMock
                .SetupGet(k => k.Id)
                .Returns(1);

            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                db.AddOrUpdate(keyMock.Object, null, null);
            });
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore AddOrUpdate with null key should throw ArgumentNullException with correct ParamName.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_AddOrUpdateWithNullKey_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var valueMock = new Mock<IValueModel>();
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                db.AddOrUpdate(null, k => valueMock.Object, (k, v) => valueMock.Object);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore AddOrUpdate with null key and null addValueFactory should throw ArgumentNullException.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_AddOrUpdateWithNullKeyAndNullAddValueFactory_ShouldThrowArgumentNullException()
        {
            // Arrange
            var valueMock = new Mock<IValueModel>();
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                db.AddOrUpdate(null, null, (k, v) => valueMock.Object);
            });
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore AddOrUpdate with null key and null updateValueFactory should throw ArgumentNullException.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_AddOrUpdateWithNullKeyAndNullUpdateValueFactory_ShouldThrowArgumentNullException()
        {
            // Arrange
            var valueMock = new Mock<IValueModel>();
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                db.AddOrUpdate(null, k => valueMock.Object, null);
            });
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore AddOrUpdate with null updateValueFactory should throw ArgumentNullException with correct ParamName.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_AddOrUpdateWithNullUpdateValueFactory_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var keyMock = new Mock<IKeyModel>();
            keyMock
                .SetupGet(k => k.Id)
                .Returns(1);

            var valueMock = new Mock<IValueModel>();
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                db.AddOrUpdate(keyMock.Object, k => valueMock.Object, null);
            });

            Assert.AreEqual(Constants.UpdateValueFactoryParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore Index with null key should throw ArgumentNullException with correct ParamName.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_IndexWithNullKey_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var value = db[null];
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore invocation of Keys should not throw.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_InvocationOfKeys_ShouldNotThrow()
        {
            // Arrange
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act
            var keys = db.Keys;

            // Assert
            Assert.IsNotNull(keys);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore Remove with null key should throw ArgumentNullException with correct ParamName.")]
        [Timeout(500)]
        public void MemoryKeyValueDataStore_RemoveWithNullKey_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                db.Remove(null);
            });

            Assert.AreEqual(Constants.KeyParamName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(MemoryKeyValueDataStore<IKeyModel, IValueModel>), Description = "MemoryKeyValueDataStore with default constructor should initialize object correctly.")]
        [Timeout(100)]
        public void MemoryKeyValueDataStore_WithDefaultConstructor_ShouldInitializeObjectCorrectly()
        {
            // Act + Assert
            var db = new MemoryKeyValueDataStore<IKeyModel, IValueModel>();

            Assert.IsNotNull(db);
        }
    }
}
