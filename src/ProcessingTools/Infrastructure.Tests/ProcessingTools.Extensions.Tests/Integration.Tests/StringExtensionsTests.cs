// <copyright file="StringExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Integration.Tests
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// String extensions tests.
    /// </summary>
    [TestClass]
    public class StringExtensionsTests
    {
        private readonly string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        /// <summary>
        /// ConvertToT should convert <see cref="string"/>to <see cref="int"/>.
        /// </summary>
        [TestMethod]
        public void ConvertToT_ShouldConvertStringToInt()
        {
            Assert.AreEqual(1, "1".ConvertTo<int>(), "1 should be int.");
            Assert.AreEqual(-10, "-10".ConvertTo<int>(), "-10 should be int.");
        }

        /// <summary>
        /// ConvertToT should convert <see cref="string"/>to <see cref="double"/>.
        /// </summary>
        [TestMethod]
        public void ConvertToT_ShouldConvertStringToDouble()
        {
            Assert.AreEqual(
                1.0,
                "1.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo<double>(),
                "1.0 should be double.");

            Assert.AreEqual(
                -10.0,
                "-10.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo<double>(),
                "-10.0 should be double.");
        }

        /// <summary>
        /// ConvertToType should convert <see cref="string"/>to <see cref="int"/>.
        /// </summary>
        [TestMethod]
        public void ConvertToType_ShouldConvertStringToInt()
        {
            Assert.AreEqual(1, "1".ConvertTo(typeof(int)), "1 should be int.");
            Assert.AreEqual(-10, "-10".ConvertTo(typeof(int)), "-10 should be int.");
        }

        /// <summary>
        /// ConvertToType should convert <see cref="string"/>to <see cref="double"/>.
        /// </summary>
        [TestMethod]
        public void ConvertToType_ShouldConvertStringToDouble()
        {
            Assert.AreEqual(
                1.0,
                "1.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo(typeof(double)),
                "1.0 should be double.");

            Assert.AreEqual(
                -10.0,
                "-10.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo(typeof(double)),
                "-10.0 should be double.");
        }
    }
}
