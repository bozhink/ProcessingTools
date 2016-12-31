namespace ProcessingTools.Strings.Tests.Unit.Tests.Extensions
{
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using ProcessingTools.Strings.Extensions;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Unit")]
    public class DistinctWithStopWordsUnitTests
    {
        [Test(Author = "Bozhin Karaivanov", Description = "DistinctWithStopWords with null words should return valid IEnumerable")]
        public void DistinctWithStopWords_WithNullWords_ShouldReturnValidIEnumerable()
        {
            // Arrange
            var stopWordsMock = new Mock<IEnumerable<string>>();
            IEnumerable<string> words = null;

            // Act
            var result = words.DistinctWithStopWords(stopWordsMock.Object);

            // Assert
            Assert.IsNotNull(result);
            stopWordsMock.Verify(s => s.GetEnumerator(), Times.Never);
        }

        [Test(Author = "Bozhin Karaivanov", Description = "DistinctWithStopWords with null stopWords should return the original words")]
        public void DistinctWithStopWords_WithNullStopWords_ShouldReturnTheOriginalWords()
        {
            // Arrange
            var wordsMock = new Mock<IEnumerable<string>>();

            // Act
            var result = wordsMock.Object.DistinctWithStopWords(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(wordsMock.Object, result);
            wordsMock.Verify(s => s.GetEnumerator(), Times.Never);
        }
    }
}
