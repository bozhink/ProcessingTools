namespace ProcessingTools.Harvesters.Tests.Unit.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Harvesters.Abbreviations;
    using ProcessingTools.Models.Contracts.Harvesters.Abbreviations;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.Abbreviations;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(AbbreviationsHarvester))]
    public class AbbreviationsHarvesterUnitTests
    {
        private const string HarvesterCoreFieldName = "harvesterCore";
        private const string SerializerFieldName = "serializer";
        private const string TransformersFactoryFieldName = "transformersFactory";
        private static readonly Type HarvesterType = typeof(AbbreviationsHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void AbbreviationsHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IAbbreviationModel>>();
            var harvesterCore = harvesterCoreMock.Object;

            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();

            // Act
            var harvester = new AbbreviationsHarvester(harvesterCore, serializerMock.Object, transformersFactoryMock.Object);

            // Assert
            Assert.IsNotNull(harvester);

            var harvesterCoreField = PrivateField.GetInstanceField(HarvesterType, harvester, HarvesterCoreFieldName);
            Assert.IsNotNull(harvesterCoreField);
            Assert.IsInstanceOf<IEnumerableXmlHarvesterCore<IAbbreviationModel>>(harvesterCoreField);
            Assert.AreSame(harvesterCore, harvesterCoreField);

            var serializerField = PrivateField.GetInstanceField(HarvesterType, harvester, SerializerFieldName);
            Assert.IsNotNull(serializerField);
            Assert.IsInstanceOf<IXmlTransformDeserializer>(serializerField);
            Assert.AreSame(serializerMock.Object, serializerField);

            var transformersFactoryField = PrivateField.GetInstanceField(HarvesterType, harvester, TransformersFactoryFieldName);
            Assert.IsNotNull(transformersFactoryField);
            Assert.IsInstanceOf<IAbbreviationsTransformersFactory>(transformersFactoryField);
            Assert.AreSame(transformersFactoryMock.Object, transformersFactoryField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null harvesterCore in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullHarvesterCoreInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(null, serializerMock.Object, transformersFactoryMock.Object);
            });

            Assert.AreEqual(HarvesterCoreFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null transformer factory in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullTransformerFactoryInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IAbbreviationModel>>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(harvesterCoreMock.Object, serializerMock.Object, null);
            });

            Assert.AreEqual(TransformersFactoryFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null serializer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullSerializerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IAbbreviationModel>>();
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(harvesterCoreMock.Object, null, transformersFactoryMock.Object);
            });

            Assert.AreEqual(SerializerFieldName, exception.ParamName);
        }
    }
}
