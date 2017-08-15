namespace ProcessingTools.Strings.Tests.Integration.Tests
{
    using NUnit.Framework;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(LevenshteinDistance))]
    public class LevenshteinDistanceIntegrationTests
    {
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
