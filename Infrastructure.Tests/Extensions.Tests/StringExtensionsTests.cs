namespace ProcessingTools.Extensions.Tests
{
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionsTests
    {
        private readonly string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        [TestMethod]
        public void ConvertToT_ShouldConvertStringToInt()
        {
            Assert.AreEqual(1, "1".ConvertTo<int>(), "1 should be int.");
            Assert.AreEqual(-10, "-10".ConvertTo<int>(), "-10 should be int.");
        }

        [TestMethod]
        public void ConvertToT_ShouldConvertStringToDouble()
        {
            Assert.AreEqual(
                1.0,
                "1.0".Replace(".", this.decimalSeparator).ConvertTo<double>(),
                "1.0 should be double.");

            Assert.AreEqual(
                -10.0,
                "-10.0".Replace(".", this.decimalSeparator).ConvertTo<double>(),
                "-10.0 should be double.");
        }

        [TestMethod]
        public void ConvertToType_ShouldConvertStringToInt()
        {
            Assert.AreEqual(1, "1".ConvertTo(typeof(int)), "1 should be int.");
            Assert.AreEqual(-10, "-10".ConvertTo(typeof(int)), "-10 should be int.");
        }

        [TestMethod]
        public void ConvertToType_ShouldConvertStringToDouble()
        {
            Assert.AreEqual(
                1.0,
                "1.0".Replace(".", this.decimalSeparator).ConvertTo(typeof(double)),
                "1.0 should be double.");

            Assert.AreEqual(
                -10.0,
                "-10.0".Replace(".", this.decimalSeparator).ConvertTo(typeof(double)),
                "-10.0 should be double.");
        }
    }
}