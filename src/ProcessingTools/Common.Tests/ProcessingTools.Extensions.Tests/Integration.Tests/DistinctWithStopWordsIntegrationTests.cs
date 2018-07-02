// <copyright file="DistinctWithStopWordsIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Integration.Tests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Distinct with stop words integration tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration")]
    public class DistinctWithStopWordsIntegrationTests
    {
        /// <summary>
        /// DistinctWithStopWords with valid parameters should return valid IEnumerable.
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", Description = "DistinctWithStopWords with valid parameters should return valid IEnumerable")]
        public void DistinctWithStopWords_WithValidParameters_ShouldReturnValidIEnumerable()
        {
            // Arrange
            var stopWords = new List<string>
            {
                "item"
            };

            var words = new List<string>();

            // Act
            var result = words.DistinctWithStopWords(stopWords);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotSame(words, result);
        }

        /// <summary>
        /// DistinctWithStopWords with empty stopWords should return input words
        /// </summary>
        [Test(Author = "Bozhin Karaivanov", Description = "DistinctWithStopWords with empty stopWords should return input words")]
        public void DistinctWithStopWords_WithEmptyStopWords_ShouldReturnInputWords()
        {
            // Arrange
            var stopWords = new List<string>();

            var words = new List<string>
            {
                "item1"
            };

            // Act
            var result = words.DistinctWithStopWords(stopWords);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(words, result);
        }
    }
}
