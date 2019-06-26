// <copyright file="RegexExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Integration.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Regex extensions tests.
    /// </summary>
    [TestClass]
    public class RegexExtensionsTests
    {
        /// <summary>
        /// Gets or sets the <see cref="TestContext"/>.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// MatchWordsInString.AsEnumerable should return <see cref="IEnumerable{T}"/> of word strings.
        /// </summary>
        [TestMethod]
        public void MatchWordsInString_AsEnumerable_ShouldReturnIEnumerableOfWordStrings()
        {
            const string Text = "The following example has a yield return statement that's inside a for loop. Each iteration of the foreach statement body in Process creates a call to the Power iterator function. Each call to the iterator function proceeds to the next execution of the yield return statement, which occurs during the next iteration of the for loop.";

            Regex matchWord = new Regex(@"[^\W\d]+");

            var words = matchWord.Match(Text).AsEnumerable().ToList();

            Assert.IsTrue(words.Any());

            foreach (var word in words)
            {
                this.TestContext.WriteLine(word);
            }
        }
    }
}
