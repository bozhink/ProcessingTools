namespace ProcessingTools.Harvesters.Tests.Unit.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Harvesters.Contracts.Transformers;
    using ProcessingTools.Harvesters.Harvesters.Abbreviations;
    using ProcessingTools.Tests.Library;
    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Serialization;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(AbbreviationsHarvester))]
    public class AbbreviationsHarvesterUnitTests
    {
        private const string ContextWrapperProviderFieldName = "contextWrapperProvider";
        private const string SerializerFieldName = "serializer";
        private const string TransformerFieldName = "transformer";
        private static readonly Type HarvesterType = typeof(AbbreviationsHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void AbbreviationsHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var contextWrapperProviderMock = new Mock<IXmlContextWrapperProvider>();
            var contextWrapperProvider = contextWrapperProviderMock.Object;

            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformerMock = new Mock<IGetAbbreviationsTransformer>();

            // Act
            var harvester = new AbbreviationsHarvester(contextWrapperProvider, serializerMock.Object, transformerMock.Object);

            // Assert
            Assert.IsNotNull(harvester);

            var contextWrapperProviderField = PrivateField.GetInstanceField(HarvesterType.BaseType, harvester, ContextWrapperProviderFieldName);
            Assert.IsNotNull(contextWrapperProviderField);
            Assert.IsInstanceOf<IXmlContextWrapperProvider>(contextWrapperProviderField);
            Assert.AreSame(contextWrapperProvider, contextWrapperProviderField);

            var serializerField = PrivateField.GetInstanceField(HarvesterType, harvester, SerializerFieldName);
            Assert.IsNotNull(serializerField);
            Assert.IsInstanceOf<IXmlTransformDeserializer>(serializerField);
            Assert.AreSame(serializerMock.Object, serializerField);

            var transformerField = PrivateField.GetInstanceField(HarvesterType, harvester, TransformerFieldName);
            Assert.IsNotNull(transformerField);
            Assert.IsInstanceOf<IGetAbbreviationsTransformer>(transformerField);
            Assert.AreSame(transformerMock.Object, transformerField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null contextWrapperProvider in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullContextWrapperProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformerMock = new Mock<IGetAbbreviationsTransformer>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var harvester = new AbbreviationsHarvester(null, serializerMock.Object, transformerMock.Object);
            });

            Assert.AreEqual(ContextWrapperProviderFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null transformer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullTransformerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var contextWrapperProviderMock = new Mock<IXmlContextWrapperProvider>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var harvester = new AbbreviationsHarvester(contextWrapperProviderMock.Object, serializerMock.Object, null);
            });

            Assert.AreEqual(TransformerFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null serializer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullSerializerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var contextWrapperProviderMock = new Mock<IXmlContextWrapperProvider>();
            var transformerMock = new Mock<IGetAbbreviationsTransformer>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var harvester = new AbbreviationsHarvester(contextWrapperProviderMock.Object, null, transformerMock.Object);
            });

            Assert.AreEqual(SerializerFieldName, exception.ParamName);
        }
    }
}
