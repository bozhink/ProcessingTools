// <copyright file="RegexExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Text.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using NUnit.Framework;

    /// <summary>
    /// Regex extensions tests.
    /// </summary>
    [TestFixture(TestOf = typeof(RegexExtensions), Category = "Unit")]
    public class RegexExtensionsTests
    {
        /// <summary>
        /// Gets or sets the <see cref="TestContext"/>.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// MatchWordsInString.AsEnumerable should return <see cref="IEnumerable{T}"/> of word strings.
        /// </summary>
        [Test(TestOf = typeof(RegexExtensions), Description = "MatchWordsInString.AsEnumerable should return IEnumerable{T} of word strings")]
        public void MatchWordsInString_AsEnumerable_ShouldReturnIEnumerableOfWordStrings()
        {
            // Arrange
            string text = "The following example has a yield return statement that's inside a for loop. Each iteration of the foreach statement body in Process creates a call to the Power iterator function. Each call to the iterator function proceeds to the next execution of the yield return statement, which occurs during the next iteration of the for loop.";

            Regex matchWord = new Regex(@"[^\W\d]+");

            // Act
            var words = matchWord.Match(text).AsEnumerable().ToList();

            // Assert
            Assert.IsTrue(words.Any());

            foreach (var word in words)
            {
                TestContext.WriteLine(word);
            }
        }
    }
}
