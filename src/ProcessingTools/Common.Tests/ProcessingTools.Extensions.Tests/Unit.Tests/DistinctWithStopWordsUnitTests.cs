// <copyright file="DistinctWithStopWordsUnitTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Unit.Tests
{
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Distinct with stop words unit tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit")]
    public class DistinctWithStopWordsUnitTests
    {
        /// <summary>
        /// DistinctWithStopWords with null words should return valid IEnumerable.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", Description = "DistinctWithStopWords with null words should return valid IEnumerable")]
        public void DistinctWithStopWords_WithNullWords_ShouldReturnValidIEnumerable()
        {
            // Arrange
            var stopWordsMock = new Mock<IEnumerable<string>>();

            // Act
            var result = StringExtensions.DistinctWithStopWords(null, stopWordsMock.Object);

            // Assert
            Assert.IsNotNull(result);
            stopWordsMock.Verify(s => s.GetEnumerator(), Times.Never);
        }

        /// <summary>
        /// DistinctWithStopWords with null stopWords should return the original words
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", Description = "DistinctWithStopWords with null stopWords should return the original words")]
        public void DistinctWithStopWords_WithNullStopWords_ShouldReturnTheOriginalWords()
        {
            // Arrange
            var wordsMock = new Mock<IEnumerable<string>>();

            // Act
            var result = StringExtensions.DistinctWithStopWords(wordsMock.Object, null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(wordsMock.Object, result);
            wordsMock.Verify(s => s.GetEnumerator(), Times.Never);
        }
    }
}
