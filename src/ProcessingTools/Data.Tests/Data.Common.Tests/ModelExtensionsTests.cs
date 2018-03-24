namespace ProcessingTools.Data.Common.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Data.Common.Tests.Attributes;
    using ProcessingTools.Data.Common.Tests.Models;
    using ProcessingTools.Extensions.Data;

    [TestClass]
    public class ModelExtensionsTests
    {
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
