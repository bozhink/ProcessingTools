// <copyright file="ListIntersectionsExtensionsTests.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Integration.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Extensions;

    /// <summary>
    /// List Intersections Extensions Tests
    /// </summary>
    [TestClass]
    public class ListIntersectionsExtensionsTests
    {
        private static List<string> wordList;

        /// <summary>
        /// Class Initialize.
        /// </summary>
        /// <param name="context">Test context.</param>
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var words = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "City", "city", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England", "FA", "far", "fire", "football", "Football", "football", "for", "from" };

            wordList = words.ToList();
        }

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_NonRegex_CaseInsensitive_StrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseInsensitive = wordList
                .MatchWithStringList(matches, false, false, true);

            Assert.AreEqual(
                "east football Football",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive non-regex match failed.");
        }

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_NonRegex_CaseSensitive_StrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseSesitive = wordList
                .MatchWithStringList(matches, false, true, true);

            Assert.AreEqual(
                "east football",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive non-regex match failed.");
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_Regex_CaseInsensitive_StrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseInsensitive = wordList
                .MatchWithStringList(matches, true, false, true);

            Assert.AreEqual(
                "east football Football",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive regex match failed.");
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_Regex_CaseSensitive_StrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseSesitive = wordList
                .MatchWithStringList(matches, true, true, true);

            Assert.AreEqual(
                "east football",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive regex match failed.");
        }

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_NonRegex_CaseInsensitive_NonStrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseInsensitive = wordList
                .MatchWithStringList(matches, false, false, false);

            Assert.AreEqual(
                "east football Football",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive non-regex match failed.");
        }

        /// <summary>
        /// MatchWithStringList - Non Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_NonRegex_CaseSensitive_NonStrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseSesitive = wordList
                .MatchWithStringList(matches, false, true, false);

            Assert.AreEqual(
                "east football",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive non-regex match failed.");
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_Regex_CaseInsensitive_NonStrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseInsensitive = wordList
                .MatchWithStringList(matches, true, false, false);

            Assert.AreEqual(
                "east football Football",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive regex match failed.");
        }

        /// <summary>
        /// MatchWithStringList - Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void MatchWithStringList_Regex_CaseSensitive_NonStrictMode()
        {
            var matches = new[] { "east", "football" };
            var matchedValuesCaseSesitive = wordList
                .MatchWithStringList(matches, true, true, false);

            Assert.AreEqual(
                "east football",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive regex match failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_NonRegex_CaseInsensitive_StrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseInsensitive = wordList
                .DistinctWithStringList(matches, false, false, true);

            Assert.AreEqual(
                "City city FA far fire football Football for from",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive non-regex distinct failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_NonRegex_CaseSensitive_StrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseSesitive = wordList
                .DistinctWithStringList(matches, false, true, true);

            Assert.AreEqual(
                "City city FA far fire football Football for from",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive non-regex distinct failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Insensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_Regex_CaseInsensitive_StrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseInsensitive = wordList
                .DistinctWithStringList(matches, true, false, true);

            Assert.AreEqual(
                "City city FA far fire football Football for from",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive regex distinct failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Sensitive - Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_Regex_CaseSensitive_StrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseSesitive = wordList
                .DistinctWithStringList(matches, true, true, true);

            Assert.AreEqual(
                "City city FA far fire football Football for from",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive regex distinct failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_NonRegex_CaseInsensitive_NonStrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseInsensitive = wordList
                .DistinctWithStringList(matches, false, false, false);

            Assert.AreEqual(
                "City city fire for from",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive non-regex distinct failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Non Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_NonRegex_CaseSensitive_NonStrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseSesitive = wordList
                .DistinctWithStringList(matches, false, true, false);

            Assert.AreEqual(
                "City city FA fire for from",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive non-regex distinct failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Insensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_Regex_CaseInsensitive_NonStrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseInsensitive = wordList
                .DistinctWithStringList(matches, true, false, false);

            Assert.AreEqual(
                "City city FA far fire football Football for from",
                string.Join(" ", matchedValuesCaseInsensitive),
                "CaseInsensitive regex distinct failed.");
        }

        /// <summary>
        /// DistinctWithStringList - Regex - Case Sensitive - Non Strict Mode.
        /// </summary>
        [TestMethod]
        public void DistinctWithStringList_Regex_CaseSensitive_NonStrictMode()
        {
            var matches = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England" };
            var matchedValuesCaseSesitive = wordList
                .DistinctWithStringList(matches, true, true, false);

            Assert.AreEqual(
                "City city FA far fire football Football for from",
                string.Join(" ", matchedValuesCaseSesitive),
                "CaseSensitive regex distinct failed.");
        }
    }
}
