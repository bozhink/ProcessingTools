﻿// <copyright file="DocumentValidatorTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Unit.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ProcessingTools.Common.Code;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Models;

    /// <summary>
    /// <see cref="DocumentValidator"/> Tests.
    /// </summary>
    [TestClass]
    public class DocumentValidatorTests
    {
        /// <summary>
        /// <see cref="DocumentValidator"/>: validate sample XML should work.
        /// </summary>
        [TestMethod]
        [Ignore] // Not implemented
        public void DocumentValidator_ValidateSampleXml_ShouldWork()
        {
            // Arrange
            var reporterMock = new Mock<IReporter>();
            var validator = new DocumentValidator();
            var document = new TaxPubDocument
            {
                Xml = "<article><front></front></article>",
            };

            // Act
            validator.ValidateAsync(document, reporterMock.Object).Wait();

            // Assert
            reporterMock.VerifyAll();
        }
    }
}
