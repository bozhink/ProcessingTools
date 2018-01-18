namespace ProcessingTools.Harvesters.Tests.Unit.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Harvesters.ExternalLinks;
    using ProcessingTools.Contracts.Models.Harvesters.ExternalLinks;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.ExternalLinks;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(ExternalLinksHarvester))]
    public class ExternalLinksHarvesterUnitTests
    {
        private const string HarvesterCoreFieldName = "harvesterCore";
        private const string SerializerFieldName = "serializer";
        private const string TransformersFactoryFieldName = "transformersFactory";
        private static readonly Type HarvesterType = typeof(ExternalLinksHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void ExternalLinksHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IExternalLinkModel>>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IExternalLinksTransformersFactory>();

            // Act
            var harvester = new ExternalLinksHarvester(harvesterCoreMock.Object, serializerMock.Object, transformersFactoryMock.Object);

            // Assert
            Assert.IsNotNull(harvester);

            var harvesterCoreField = PrivateField.GetInstanceField(HarvesterType, harvester, HarvesterCoreFieldName);
            Assert.IsNotNull(harvesterCoreField);
            Assert.IsInstanceOf<IEnumerableXmlHarvesterCore<IExternalLinkModel>>(harvesterCoreField);
            Assert.AreSame(harvesterCoreMock.Object, harvesterCoreField);

            var serializerField = PrivateField.GetInstanceField(HarvesterType, harvester, SerializerFieldName);
            Assert.IsNotNull(serializerField);
            Assert.IsInstanceOf<IXmlTransformDeserializer>(serializerField);
            Assert.AreSame(serializerMock.Object, serializerField);

            var transformersFactoryField = PrivateField.GetInstanceField(HarvesterType, harvester, TransformersFactoryFieldName);
            Assert.IsNotNull(transformersFactoryField);
            Assert.IsInstanceOf<IExternalLinksTransformersFactory>(transformersFactoryField);
            Assert.AreSame(transformersFactoryMock.Object, transformersFactoryField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null harvesterCore in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullHarvesterCoreInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IExternalLinksTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(null, serializerMock.Object, transformersFactoryMock.Object);
            });

            Assert.AreEqual(HarvesterCoreFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null transformer factory in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullTransformerFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IExternalLinkModel>>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(harvesterCoreMock.Object, serializerMock.Object, null);
            });

            Assert.AreEqual(TransformersFactoryFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null serializer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullSerializerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IExternalLinkModel>>();
            var transformersFactoryMock = new Mock<IExternalLinksTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(harvesterCoreMock.Object, null, transformersFactoryMock.Object);
            });

            Assert.AreEqual(SerializerFieldName, exception.ParamName);
        }
    }
}
