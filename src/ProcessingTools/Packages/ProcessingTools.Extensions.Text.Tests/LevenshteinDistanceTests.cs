// <copyright file="LevenshteinDistanceTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

// See https://www.datacamp.com/community/tutorials/fuzzy-string-python
namespace ProcessingTools.Extensions.Text.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Levenshtein distance integration tests.
    /// </summary>
    [TestFixture(Category = "Integration", TestOf = typeof(LevenshteinDistance))]
    public class LevenshteinDistanceTests
    {
        /// <summary>
        /// Levenshtein distance compute with valid strings should work.
        /// </summary>
        /// <param name="string1">Left string to compare.</param>
        /// <param name="string2">Right string to compare.</param>
        /// <param name="distance">Expected distance.</param>
        [Test(TestOf = typeof(LevenshteinDistance), Description = "LevenshteinDistance.Compute with valid strings should work.")]
        [TestCase("ant", "ant", 0)]
        [TestCase("aunt", "ant", 1)]
        [TestCase("Sam", "Samantha", 5)]
        [TestCase("flomax", "volmax", 3)]
        [TestCase("Apple Inc.", "apple Inc", 2)]
        public void LevenshteinDistance_ComputeWithValidStrings_ShouldWork(string string1, string string2, int distance)
        {
            // Act
            var result = LevenshteinDistance.Compute(string1, string2);

            // Assert
            Assert.AreEqual(distance, result);
        }

        /// <summary>
        /// Levenshtein distance compute is commutative.
        /// </summary>
        /// <param name="leftString">Left string to compare.</param>
        /// <param name="rightString">Right string to compare.</param>
        [Test(TestOf = typeof(LevenshteinDistance), Description = "LevenshteinDistance.Compute is commutative.")]
        [TestCase("ant", "ant")]
        [TestCase("aunt", "ant")]
        [TestCase("Sam", "Samantha")]
        [TestCase("flomax", "volmax")]
        [TestCase("Apple Inc.", "apple Inc")]
        public void LevenshteinDistance_Compute_IsCommutative(string leftString, string rightString)
        {
            // Act
            var distanceLeft = LevenshteinDistance.Compute(leftString, rightString);
            var distanceRight = LevenshteinDistance.Compute(rightString, leftString);

            // Assert
            Assert.AreEqual(distanceLeft, distanceRight);
        }

        /// <summary>
        /// Liechtenstein distance compute similarity ration should work.
        /// </summary>
        /// <param name="leftString">Left string to compare.</param>
        /// <param name="rightString">Right string to compare.</param>
        [Test(TestOf = typeof(LevenshteinDistance), Description = "LevenshteinDistance.ComputeSimilarityRatio is should work.")]
        [TestCase("ant", "ant")]
        [TestCase("aunt", "ant")]
        [TestCase("Sam", "Samantha")]
        [TestCase("flomax", "volmax")]
        [TestCase("Apple Inc.", "apple Inc")]
        public void LevenshteinDistance_ComputeSimilarityRatio_ShouldWork(string leftString, string rightString)
        {
            // Act
            var distance = LevenshteinDistance.ComputeSimilarityRatio(leftString, rightString);

            TestContext.WriteLine("lev('{0}', '{1}') = {2}", leftString, rightString, distance);

            // Assert
            Assert.IsTrue(distance > 0);
        }
    }
}
