namespace ProcessingTools.Harvesters.Tests.Unit.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.Abbreviations;
    using ProcessingTools.Harvesters.Contracts;
    using ProcessingTools.Harvesters.Contracts.Abbreviations;
    using ProcessingTools.Harvesters.Models.Contracts.Abbreviations;
    using ProcessingTools.Tests.Library;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(AbbreviationsHarvester))]
    public class AbbreviationsHarvesterUnitTests
    {
        private const string HarvesterCoreFieldName = "harvesterCore";
        private const string SerializerFieldName = "serializer";
        private const string TransformerFactoryFieldName = "transformerFactory";
        private static readonly Type HarvesterType = typeof(AbbreviationsHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void AbbreviationsHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IAbbreviationModel>>();
            var harvesterCore = harvesterCoreMock.Object;

            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformerFactoryMock = new Mock<IAbbreviationsTransformerFactory>();

            // Act
            var harvester = new AbbreviationsHarvester(harvesterCore, serializerMock.Object, transformerFactoryMock.Object);

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

            var transformerFactoryField = PrivateField.GetInstanceField(HarvesterType, harvester, TransformerFactoryFieldName);
            Assert.IsNotNull(transformerFactoryField);
            Assert.IsInstanceOf<IAbbreviationsTransformerFactory>(transformerFactoryField);
            Assert.AreSame(transformerFactoryMock.Object, transformerFactoryField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null harvesterCore in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullHarvesterCoreInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformerFactoryMock = new Mock<IAbbreviationsTransformerFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(null, serializerMock.Object, transformerFactoryMock.Object);
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

            Assert.AreEqual(TransformerFactoryFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null serializer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullSerializerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IAbbreviationModel>>();
            var transformerFactoryMock = new Mock<IAbbreviationsTransformerFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(harvesterCoreMock.Object, null, transformerFactoryMock.Object);
            });

            Assert.AreEqual(SerializerFieldName, exception.ParamName);
        }
    }
}
