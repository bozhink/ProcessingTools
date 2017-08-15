namespace ProcessingTools.Harvesters.Tests.Unit.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Harvesters.Contracts.Factories;
    using ProcessingTools.Harvesters.Harvesters.Abbreviations;
    using ProcessingTools.Tests.Library;
    using ProcessingTools.Xml.Contracts.Serialization;
    using ProcessingTools.Xml.Contracts.Wrappers;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(AbbreviationsHarvester))]
    public class AbbreviationsHarvesterUnitTests
    {
        private const string ContextWrapperFieldName = "contextWrapper";
        private const string SerializerFieldName = "serializer";
        private const string TransformersFactoryFieldName = "transformersFactory";
        private static readonly Type HarvesterType = typeof(AbbreviationsHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void AbbreviationsHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var contextWrapperMock = new Mock<IXmlContextWrapper>();
            var contextWrapper = contextWrapperMock.Object;

            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();

            // Act
            var harvester = new AbbreviationsHarvester(contextWrapper, serializerMock.Object, transformersFactoryMock.Object);

            // Assert
            Assert.IsNotNull(harvester);

            var contextWrapperField = PrivateField.GetInstanceField(HarvesterType.BaseType, harvester, ContextWrapperFieldName);
            Assert.IsNotNull(contextWrapperField);
            Assert.IsInstanceOf<IXmlContextWrapper>(contextWrapperField);
            Assert.AreSame(contextWrapper, contextWrapperField);

            var serializerField = PrivateField.GetInstanceField(HarvesterType, harvester, SerializerFieldName);
            Assert.IsNotNull(serializerField);
            Assert.IsInstanceOf<IXmlTransformDeserializer>(serializerField);
            Assert.AreSame(serializerMock.Object, serializerField);

            var transformersFactoryField = PrivateField.GetInstanceField(HarvesterType, harvester, TransformersFactoryFieldName);
            Assert.IsNotNull(transformersFactoryField);
            Assert.IsInstanceOf<IAbbreviationsTransformersFactory>(transformersFactoryField);
            Assert.AreSame(transformersFactoryMock.Object, transformersFactoryField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null contextWrapper in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullContextWrapperInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(null, serializerMock.Object, transformersFactoryMock.Object);
            });

            Assert.AreEqual(ContextWrapperFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null transformer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullTransformerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var contextWrapperMock = new Mock<IXmlContextWrapper>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(contextWrapperMock.Object, serializerMock.Object, null);
            });

            Assert.AreEqual(TransformersFactoryFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null serializer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullSerializerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var contextWrapperMock = new Mock<IXmlContextWrapper>();
            var transformersFactoryMock = new Mock<IAbbreviationsTransformersFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new AbbreviationsHarvester(contextWrapperMock.Object, null, transformersFactoryMock.Object);
            });

            Assert.AreEqual(SerializerFieldName, exception.ParamName);
        }
    }
}
