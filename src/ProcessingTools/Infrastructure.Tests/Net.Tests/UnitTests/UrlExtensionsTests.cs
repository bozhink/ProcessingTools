namespace ProcessingTools.Net.Tests.UnitTests
{
    using NUnit.Framework;
    using ProcessingTools.Common.Extensions;

    [TestFixture]
    public class UrlExtensionsTests
    {
        [TestCase("", "")]
        [TestCase("Coleoptera", "Coleoptera")]
        [TestCase(" ", "+")]
        [TestCase("      ", "++++++")]
        [TestCase("Férussac", "F%C3%A9russac")]
        [TestCase("Input a string of text and encode or decode it as you like.", "Input+a+string+of+text+and+encode+or+decode+it+as+you+like.")]
        [TestCase("http://meyerweb.com/eric/tools/dencoder/", "http%3A%2F%2Fmeyerweb.com%2Feric%2Ftools%2Fdencoder%2F")]
        [TestCase(@"(({}}}[[]]}}{}{}))))}{}{}{};""""'''''‘’‘’‘’‘’!@#!@#@(#$)#)%*)$%*)#$%+_)(*&^%$#@!~!@#@$%^&*(~~/.,", "((%7B%7D%7D%7D%5B%5B%5D%5D%7D%7D%7B%7D%7B%7D))))%7D%7B%7D%7B%7D%7B%7D%3B%22%22%27%27%27%27%27%E2%80%98%E2%80%99%E2%80%98%E2%80%99%E2%80%98%E2%80%99%E2%80%98%E2%80%99!%40%23!%40%23%40(%23%24)%23)%25*)%24%25*)%23%24%25%2B_)(*%26%5E%25%24%23%40!%7E!%40%23%40%24%25%5E%26*(%7E%7E%2F.%2C")]
        public void UrlEncode_WithNonNullString_ShouldWork(string input, string expectedResult)
        {
            Assert.AreEqual(expectedResult, input.UrlEncode(), "Encode strings should match.");
        }

        [TestCase("", "")]
        [TestCase("Coleoptera", "Coleoptera")]
        [TestCase("+", " ")]
        [TestCase("++++++", "      ")]
        [TestCase("F%C3%A9russac", "Férussac")]
        [TestCase("Input+a+string+of+text+and+encode+or+decode+it+as+you+like.", "Input a string of text and encode or decode it as you like.")]
        [TestCase("http%3A%2F%2Fmeyerweb.com%2Feric%2Ftools%2Fdencoder%2F", "http://meyerweb.com/eric/tools/dencoder/")]
        [TestCase("((%7B%7D%7D%7D%5B%5B%5D%5D%7D%7D%7B%7D%7B%7D))))%7D%7B%7D%7B%7D%7B%7D%3B%22%22%27%27%27%27%27%E2%80%98%E2%80%99%E2%80%98%E2%80%99%E2%80%98%E2%80%99%E2%80%98%E2%80%99!%40%23!%40%23%40(%23%24)%23)%25*)%24%25*)%23%24%25%2B_)(*%26%5E%25%24%23%40!%7E!%40%23%40%24%25%5E%26*(%7E%7E%2F.%2C", @"(({}}}[[]]}}{}{}))))}{}{}{};""""'''''‘’‘’‘’‘’!@#!@#@(#$)#)%*)$%*)#$%+_)(*&^%$#@!~!@#@$%^&*(~~/.,")]
        public void UrlDecode_WithNonNullString_ShouldWork(string input, string expectedResult)
        {
            Assert.AreEqual(expectedResult, input.UrlDecode(), "Decode strings should match.");
        }
    }
}
