// <copyright file="StringExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Text.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests for <see cref="StringExtensions"/>.
    /// </summary>
    [TestFixture(TestOf = typeof(StringExtensions), Category = "Integration")]
    public class StringExtensionsTests
    {
        private static List<string> wordList;

        /// <summary>
        /// One time set-up.
        /// </summary>
        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            var words = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "City", "city", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England", "FA", "far", "fire", "football", "Football", "football", "for", "from" };

            wordList = words.ToList();

            TestContext.WriteLine(string.Join(", ", words));
        }

        /// <summary>
        /// DistinctWithStopWords with valid parameters should return valid IEnumerable.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions), Description = "DistinctWithStopWords with valid parameters should return valid IEnumerable")]
        public void DistinctWithStopWords_WithValidParameters_ShouldReturnValidIEnumerable()
        {
            // Arrange
            var stopWords = new List<string>
            {
                "item",
            };

            var words = new List<string>();

            // Act
            var result = words.DistinctWithStopWords(stopWords);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotSame(words, result);
        }

        /// <summary>
        /// DistinctWithStopWords with empty stopWords should return input words.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions), Description = "DistinctWithStopWords with empty stopWords should return input words")]
        public void DistinctWithStopWords_WithEmptyStopWords_ShouldReturnInputWords()
        {
            // Arrange
            var stopWords = new List<string>();

            var words = new List<string>
            {
                "item1",
            };

            // Act
            var result = words.DistinctWithStopWords(stopWords);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(words, result);
        }

        /// <summary>
        /// DistinctWithStopWords with null words should return valid IEnumerable.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions), Description = "DistinctWithStopWords with null words should return valid IEnumerable")]
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
        /// DistinctWithStopWords with null stopWords should return the original words.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions), Description = "DistinctWithStopWords with null stopWords should return the original words")]
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

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_NonRegex_CaseInsensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseInsensitive = wordList.MatchWithStringList(matches, false, false, true);

            // Assert
            Assert.AreEqual("east football Football", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_NonRegex_CaseSensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseSesitive = wordList.MatchWithStringList(matches, false, true, true);

            // Assert
            Assert.AreEqual("east football", string.Join(" ", matchedValuesCaseSesitive));
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_Regex_CaseInsensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseInsensitive = wordList.MatchWithStringList(matches, true, false, true);

            // Assert
            Assert.AreEqual("east football Football", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_Regex_CaseSensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseSesitive = wordList.MatchWithStringList(matches, true, true, true);

            // Assert
            Assert.AreEqual("east football", string.Join(" ", matchedValuesCaseSesitive));
        }

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_NonRegex_CaseInsensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseInsensitive = wordList.MatchWithStringList(matches, false, false, false);

            // Assert
            Assert.AreEqual("east football Football", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_NonRegex_CaseSensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseSesitive = wordList.MatchWithStringList(matches, false, true, false);

            // Assert
            Assert.AreEqual("east football", string.Join(" ", matchedValuesCaseSesitive));
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_Regex_CaseInsensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseInsensitive = wordList.MatchWithStringList(matches, true, false, false);

            // Assert
            Assert.AreEqual("east football Football", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void MatchWithStringList_Regex_CaseSensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "east", "football" };

            // Act
            var matchedValuesCaseSesitive = wordList.MatchWithStringList(matches, true, true, false);

            // Assert
            Assert.AreEqual("east football", string.Join(" ", matchedValuesCaseSesitive));
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_NonRegex_CaseInsensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseInsensitive = wordList.DistinctWithStringList(matches, false, false, true);

            // Assert
            Assert.AreEqual("City city FA far fire football Football for from", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_NonRegex_CaseSensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseSesitive = wordList.DistinctWithStringList(matches, false, true, true);

            // Assert
            Assert.AreEqual("City city FA far fire football Football for from", string.Join(" ", matchedValuesCaseSesitive));
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_Regex_CaseInsensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseInsensitive = wordList.DistinctWithStringList(matches, true, false, true);

            // Assert
            Assert.AreEqual("City city FA far fire football Football for from", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_Regex_CaseSensitive_StrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseSesitive = wordList.DistinctWithStringList(matches, true, true, true);

            // Assert
            Assert.AreEqual("City city FA far fire football Football for from", string.Join(" ", matchedValuesCaseSesitive));
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_NonRegex_CaseInsensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseInsensitive = wordList.DistinctWithStringList(matches, false, false, false);

            // Assert
            Assert.AreEqual("City city fire for from", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_NonRegex_CaseSensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseSesitive = wordList.DistinctWithStringList(matches, false, true, false);

            // Assert
            Assert.AreEqual("City city FA fire for from", string.Join(" ", matchedValuesCaseSesitive));
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_Regex_CaseInsensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseInsensitive = wordList.DistinctWithStringList(matches, true, false, false);

            // Assert
            Assert.AreEqual("City city FA far fire football Football for from", string.Join(" ", matchedValuesCaseInsensitive));
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [Test(TestOf = typeof(StringExtensions))]
        public void DistinctWithStringList_Regex_CaseSensitive_NonStrictMode()
        {
            // Arrange
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };

            // Act
            var matchedValuesCaseSesitive = wordList.DistinctWithStringList(matches, true, true, false);

            // Assert
            Assert.AreEqual("City city FA far fire football Football for from", string.Join(" ", matchedValuesCaseSesitive));
        }
    }
}
