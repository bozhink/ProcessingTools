namespace ProcessingTools.Strings.Tests.Integration.Tests.Extensions
{
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Strings.Extensions;
    using System.Collections.Generic;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration")]
    public class DistinctWithStopWordsIntegrationTests
    {
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
