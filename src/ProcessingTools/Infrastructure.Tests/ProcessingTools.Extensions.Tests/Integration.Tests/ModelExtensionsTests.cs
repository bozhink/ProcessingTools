// <copyright file="ModelExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Integration.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Extensions.Data;
    using ProcessingTools.Extensions.Tests.Models;

    /// <summary>
    /// <see cref="ModelExtensions"/> Tests.
    /// </summary>
    [TestClass]
    public class ModelExtensionsTests
    {
        /// <summary>
        /// <see cref="ModelExtensions"/> GetIdValue on entity should work.
        /// </summary>
        [TestMethod]
        public void ModelExtensions_GetIdValueOnEntity_ShouldWork()
        {
            var entity = new Entity
            {
                Id = 1
            };

            var id = int.Parse(entity.GetIdValue().ToString());
            Assert.AreEqual(entity.Id, id, "Id should be 1.");
        }

        /// <summary>
        /// <see cref="ModelExtensions"/> GetIdValue on model with ID property should work.
        /// </summary>
        [TestMethod]
        public void ModelExtensions_GetIdValueOnModelWithIdProperty_ShouldWork()
        {
            var entity = new ModelWithIdProperty
            {
                Id = "1",
                Content = "Content"
            };

            var id = entity.GetIdValue().ToString();
            Assert.AreEqual(entity.Id, id, "Id should be 1.");
        }

        /// <summary>
        /// <see cref="ModelExtensions"/> GetIdValue on model with complex ID property should work.
        /// </summary>
        [TestMethod]
        public void ModelExtensions_GetIdValueOnModelWithComplexIdProperty_ShouldWork()
        {
            var entity = new ModelWithComplexIdProperty
            {
                ComplexId = 1
            };

            var id = int.Parse(entity.GetIdValue().ToString());
            Assert.AreEqual(entity.ComplexId, id, "Id should be 1.");
        }

        /// <summary>
        /// <see cref="ModelExtensions"/> GetIdValue on model with attribute ID property without attribute declaration should return null.
        /// </summary>
        [TestMethod]
        public void ModelExtensions_GetIdValueOnModelWithAttributeIdPropertyWithoutAttributeDeclaration_ShouldReturnNull()
        {
            var entity = new ModelWithAttributeIdProperty
            {
                IndexProperty = 1
            };

            var id = entity.GetIdValue();
            Assert.IsNull(id, "Id should be null.");
        }

        /// <summary>
        /// <see cref="ModelExtensions"/> GetIdValue on model with attribute ID property with attribute declaration should return null.
        /// </summary>
        [TestMethod]
        public void ModelExtensions_GetIdValueOnModelWithAttributeIdPropertyWithAttributeDeclaration_ShouldReturnNull()
        {
            var entity = new ModelWithAttributeIdProperty
            {
                IndexProperty = 1
            };

            var id = int.Parse(entity.GetIdValue<CustomIdAttribute>().ToString());
            Assert.AreEqual(entity.IndexProperty, id, "Id should be 1.");
        }
    }
}
