// <copyright file="LevenshteinDistanceIntegrationTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Tests.Integration.Tests.Strings
{
    using NUnit.Framework;
    using ProcessingTools.Common.Code.Strings;

    /// <summary>
    /// Levenshtein distance integration tests.
    /// </summary>
    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(LevenshteinDistance))]
    public class LevenshteinDistanceIntegrationTests
    {
        /// <summary>
        /// Levenshtein distance compute with valid strings should work.
        /// </summary>
        /// <param name="string1">Left string to compare.</param>
        /// <param name="string2">Right string to compare.</param>
        /// <param name="distance">Expected distance.</param>
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(LevenshteinDistance), Description = "LevenshteinDistance.Compute with valid strings should work.")]
        [TestCase("ant", "ant", 0)]
        [TestCase("aunt", "ant", 1)]
        [TestCase("Sam", "Samantha", 5)]
        [TestCase("flomax", "volmax", 3)]
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
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(LevenshteinDistance), Description = "LevenshteinDistance.Compute is commutative.")]
        [TestCase("ant", "ant")]
        [TestCase("aunt", "ant")]
        [TestCase("Sam", "Samantha")]
        [TestCase("flomax", "volmax")]
        public void LevenshteinDistance_Compute_IsCommutative(string leftString, string rightString)
        {
            // Act
            var distanceLeft = LevenshteinDistance.Compute(leftString, rightString);
            var distanceRight = LevenshteinDistance.Compute(rightString, leftString);

            // Assert
            Assert.AreEqual(distanceLeft, distanceRight);
        }
    }
}
