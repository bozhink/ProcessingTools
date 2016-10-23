namespace ProcessingTools.Harvesters.Tests.Unit.Tests
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Harvesters.Abbreviations;
    using ProcessingTools.Tests.Library;
    using ProcessingTools.Xml.Contracts.Providers;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(AbbreviationsHarvester))]
    public class AbbreviationsHarvesterUnitTests
    {
        private const string ContextWrapperProviderFieldName = "contextWrapperProvider";
        private static readonly Type HarvesterType = typeof(AbbreviationsHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void AbbreviationsHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var contextWrapperProviderMock = new Mock<IXmlContextWrapperProvider>();
            var contextWrapperProvider = contextWrapperProviderMock.Object;

            // Act
            var harvester = new AbbreviationsHarvester(contextWrapperProvider);

            // Assert
            Assert.IsNotNull(harvester);

            var contextWrapperProviderField = PrivateField.GetInstanceField(HarvesterType.BaseType, harvester, ContextWrapperProviderFieldName);

            Assert.IsNotNull(contextWrapperProviderField);
            Assert.IsInstanceOf<IXmlContextWrapperProvider>(contextWrapperProviderField);
            Assert.AreSame(contextWrapperProvider, contextWrapperProviderField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(AbbreviationsHarvester), Description = "AbbreviationsHarvester with null contextWrapperProvider in constructor should throw ArgumentNullException with correct ParamName.")]
        public void AbbreviationsHarvester_WithNullContextWrapperProviderInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                var harvester = new AbbreviationsHarvester(null);
            });

            Assert.AreEqual(ContextWrapperProviderFieldName, exception.ParamName);
        }
    }
}