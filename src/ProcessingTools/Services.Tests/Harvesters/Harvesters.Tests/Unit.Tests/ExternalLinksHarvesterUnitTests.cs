namespace ProcessingTools.Harvesters.Tests.Unit.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Harvesters.Contracts.Factories;
    using ProcessingTools.Harvesters.Harvesters.ExternalLinks;
    using ProcessingTools.Tests.Library;
    using ProcessingTools.Xml.Contracts.Serialization;
    using ProcessingTools.Xml.Contracts.Wrappers;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(ExternalLinksHarvester))]
    public class ExternalLinksHarvesterUnitTests
    {
        private const string ContextWrapperFieldName = "contextWrapper";
        private const string SerializerFieldName = "serializer";
        private const string TransformersFactoryFieldName = "transformersFactory";
        private static readonly Type HarvesterType = typeof(ExternalLinksHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void ExternalLinksHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var contextWrapperMock = new Mock<IXmlContextWrapper>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IExternalLinksTransformersFactory>();

            // Act
            var harvester = new ExternalLinksHarvester(contextWrapperMock.Object, serializerMock.Object, transformersFactoryMock.Object);

            // Assert
            Assert.IsNotNull(harvester);

            var contextWrapperField = PrivateField.GetInstanceField(HarvesterType.BaseType, harvester, ContextWrapperFieldName);
            Assert.IsNotNull(contextWrapperField);
            Assert.IsInstanceOf<IXmlContextWrapper>(contextWrapperField);
            Assert.AreSame(contextWrapperMock.Object, contextWrapperField);

            var serializerField = PrivateField.GetInstanceField(HarvesterType, harvester, SerializerFieldName);
            Assert.IsNotNull(serializerField);
            Assert.IsInstanceOf<IXmlTransformDeserializer>(serializerField);
            Assert.AreSame(serializerMock.Object, serializerField);

            var transformersFactoryField = PrivateField.GetInstanceField(HarvesterType, harvester, TransformersFactoryFieldName);
            Assert.IsNotNull(transformersFactoryField);
            Assert.IsInstanceOf<IExternalLinksTransformersFactory>(transformersFactoryField);
            Assert.AreSame(transformersFactoryMock.Object, transformersFactoryField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null contextWrapper in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullContextWrapperInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IExternalLinksTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(null, serializerMock.Object, transformersFactoryMock.Object);
            });

            Assert.AreEqual(ContextWrapperFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null transformer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullTransformerProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var contextWrapperMock = new Mock<IXmlContextWrapper>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(contextWrapperMock.Object, serializerMock.Object, null);
            });

            Assert.AreEqual(TransformersFactoryFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null serializer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullSerializerProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var contextWrapperMock = new Mock<IXmlContextWrapper>();
            var transformersFactoryMock = new Mock<IExternalLinksTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(contextWrapperMock.Object, null, transformersFactoryMock.Object);
            });

            Assert.AreEqual(SerializerFieldName, exception.ParamName);
        }
    }
}
