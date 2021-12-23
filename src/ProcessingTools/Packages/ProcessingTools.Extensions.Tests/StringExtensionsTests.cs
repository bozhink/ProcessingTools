// <copyright file="StringExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests
{
    using System;
    using System.Globalization;
    using NUnit.Framework;

    /// <summary>
    /// String extensions tests.
    /// </summary>
    [TestFixture]
    public class StringExtensionsTests
    {
        private readonly string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        /// <summary>
        /// ConvertToT should convert <see cref="string"/>to <see cref="int"/>.
        /// </summary>
        [Test]
        public void ConvertToT_ShouldConvertStringToInt()
        {
            Assert.AreEqual(1, "1".ConvertTo<int>());
            Assert.AreEqual(-10, "-10".ConvertTo<int>());
        }

        /// <summary>
        /// ConvertToT should convert <see cref="string"/>to <see cref="double"/>.
        /// </summary>
        [Test]
        public void ConvertToT_ShouldConvertStringToDouble()
        {
            Assert.AreEqual(1.0, "1.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo<double>());
            Assert.AreEqual(-10.0, "-10.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo<double>());
        }

        /// <summary>
        /// ConvertToType should convert <see cref="string"/>to <see cref="int"/>.
        /// </summary>
        [Test]
        public void ConvertToType_ShouldConvertStringToInt()
        {
            Assert.AreEqual(1, "1".ConvertTo(typeof(int)));
            Assert.AreEqual(-10, "-10".ConvertTo(typeof(int)));
        }

        /// <summary>
        /// ConvertToType should convert <see cref="string"/>to <see cref="double"/>.
        /// </summary>
        [Test]
        public void ConvertToType_ShouldConvertStringToDouble()
        {
            Assert.AreEqual(1.0, "1.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo(typeof(double)));
            Assert.AreEqual(-10.0, "-10.0".Replace(".", this.decimalSeparator, StringComparison.InvariantCulture).ConvertTo(typeof(double)));
        }
    }
}
