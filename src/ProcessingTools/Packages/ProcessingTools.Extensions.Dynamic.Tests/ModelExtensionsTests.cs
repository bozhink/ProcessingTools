// <copyright file="ModelExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests
{
    using System.Globalization;
    using NUnit.Framework;
    using ProcessingTools.Extensions.Dynamic.Tests.Models;

    /// <summary>
    /// Tests for models.
    /// </summary>
    [TestFixture]
    public class ModelExtensionsTests
    {
        /// <summary>
        /// <see cref="TypeExtensions"/> GetIdValue on entity should work.
        /// </summary>
        [Test]
        public void ModelExtensions_GetIdValueOnEntity_ShouldWork()
        {
            // Arrange
            var entity = new Entity
            {
                Id = 1,
            };

            // Act
            var id = int.Parse(entity.GetIdValue().ToString(), CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(entity.Id, id);
        }

        /// <summary>
        /// <see cref="TypeExtensions"/> GetIdValue on model with ID property should work.
        /// </summary>
        [Test]
        public void ModelExtensions_GetIdValueOnModelWithIdProperty_ShouldWork()
        {
            // Arrange
            var entity = new ModelWithIdProperty
            {
                Id = "1",
                Content = "Content",
            };

            // Act
            var id = entity.GetIdValue().ToString();

            // Assert
            Assert.AreEqual(entity.Id, id);
        }

        /// <summary>
        /// <see cref="TypeExtensions"/> GetIdValue on model with complex ID property should work.
        /// </summary>
        [Test]
        public void ModelExtensions_GetIdValueOnModelWithComplexIdProperty_ShouldWork()
        {
            // Arrange
            var entity = new ModelWithComplexIdProperty
            {
                ComplexId = 1,
            };

            // Act
            var id = int.Parse(entity.GetIdValue().ToString(), CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(entity.ComplexId, id);
        }

        /// <summary>
        /// <see cref="TypeExtensions"/> GetIdValue on model with attribute ID property without attribute declaration should return null.
        /// </summary>
        [Test]
        public void ModelExtensions_GetIdValueOnModelWithAttributeIdPropertyWithoutAttributeDeclaration_ShouldReturnNull()
        {
            // Arrange
            var entity = new ModelWithAttributeIdProperty
            {
                IndexProperty = 1,
            };

            // Act
            var id = entity.GetIdValue();

            // Assert
            Assert.IsNull(id);
        }

        /// <summary>
        /// <see cref="TypeExtensions"/> GetIdValue on model with attribute ID property with attribute declaration should return null.
        /// </summary>
        [Test]
        public void ModelExtensions_GetIdValueOnModelWithAttributeIdPropertyWithAttributeDeclaration_ShouldReturnNull()
        {
            // Arrange
            var entity = new ModelWithAttributeIdProperty
            {
                IndexProperty = 1,
            };

            // Act
            var id = int.Parse(entity.GetIdValue<CustomIdAttribute>().ToString(), CultureInfo.InvariantCulture);

            // Assert
            Assert.AreEqual(entity.IndexProperty, id);
        }
    }
}
