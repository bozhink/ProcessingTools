namespace ProcessingTools.Common.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Extensions;

    [TestClass]
    public class ListIntersectionsExtensionsTests
    {
        private static List<string> wordList;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            var words = new[] { "a", "accommodated", "after", "all", "also", "altered", "an", "and", "article", "at", "attendance", "attracting", "August", "becoming", "been", "before", "built", "by", "capacity", "Carrow", "City", "city", "club", "Club", "concerts", "crowd", "crowds", "Crystal", "Cup", "current", "days", "deemed", "destroyed", "devastating", "during", "east", "Elton", "England", "FA", "far", "fire", "football", "Football", "football", "for", "from" };

            wordList = words.ToList();
        }

        #region Fixed_String_Samples

        #region MatchWithStringList_StrictMode

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

        #endregion MatchWithStringList_StrictMode

        #region MatchWithStringList_NonStrictMode

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

        #endregion MatchWithStringList_NonStrictMode

        #region DistinctWithStringList_StrictMode

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

        #endregion DistinctWithStringList_StrictMode

        #region DistinctWithStringList_NonStrictMode

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

        #endregion DistinctWithStringList_NonStrictMode

        #endregion Fixed_String_Samples
    }
}
