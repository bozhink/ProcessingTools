﻿// <copyright file="ExternalLinksHarvesterUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Tests.Unit.Tests.ExternalLinks
{
    using System;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Contracts.Models.ExternalLinks;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.ExternalLinks;
    using ProcessingTools.Contracts.Services.Xml;
    using ProcessingTools.Extensions.Dynamic;
    using ProcessingTools.Services.ExternalLinks;

    [TestFixture(Category = "Unit Tests", TestOf = typeof(ExternalLinksHarvester))]
    public class ExternalLinksHarvesterUnitTests
    {
        private const string HarvesterCoreFieldName = "harvesterCore";
        private const string SerializerFieldName = "serializer";
        private const string TransformerFactoryFieldName = "transformerFactory";
        private static readonly Type HarvesterType = typeof(ExternalLinksHarvester);

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with valid parameters in constructor should correctly initialize new instance.")]
        public void ExternalLinksHarvester_WithValidParametersInConstructor_ShouldCorrectlyInitializeNewInstance()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IExternalLinkModel>>();
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformerFactoryMock = new Mock<IExternalLinksTransformerFactory>();

            // Act
            var harvester = new ExternalLinksHarvester(harvesterCoreMock.Object, serializerMock.Object, transformerFactoryMock.Object);

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

            var transformerFactoryField = PrivateField.GetInstanceField(HarvesterType, harvester, TransformerFactoryFieldName);
            Assert.IsNotNull(transformerFactoryField);
            Assert.IsInstanceOf<IExternalLinksTransformerFactory>(transformerFactoryField);
            Assert.AreSame(transformerFactoryMock.Object, transformerFactoryField);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null harvesterCore in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullHarvesterCoreInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var serializerMock = new Mock<IXmlTransformDeserializer>();
            var transformerFactoryMock = new Mock<IExternalLinksTransformerFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(null, serializerMock.Object, transformerFactoryMock.Object);
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

            Assert.AreEqual(TransformerFactoryFieldName, exception.ParamName);
        }

        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(ExternalLinksHarvester), Description = "ExternalLinksHarvester with null serializer in constructor should throw ArgumentNullException with correct ParamName.")]
        public void ExternalLinksHarvester_WithNullSerializerInConstructor_ShouldThrowArgumentNullExceptionWithCorrectParamName()
        {
            // Arrange
            var harvesterCoreMock = new Mock<IEnumerableXmlHarvesterCore<IExternalLinkModel>>();
            var transformerFactoryMock = new Mock<IExternalLinksTransformerFactory>();

            // Act + Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new ExternalLinksHarvester(harvesterCoreMock.Object, null, transformerFactoryMock.Object);
            });

            Assert.AreEqual(SerializerFieldName, exception.ParamName);
        }
    }
}
