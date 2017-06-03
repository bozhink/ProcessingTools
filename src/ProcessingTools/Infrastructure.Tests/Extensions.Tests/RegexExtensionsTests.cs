namespace ProcessingTools.Common.Extensions.Tests
{
    using System;
    using System.Text.RegularExpressions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegexExtensionsTests
    {
        [TestMethod]
        public void MatchWordsInString_ToIEnumerable_ShouldReturnIEnumerableOfWordStrings()
        {
            const string Text = "The following example has a yield return statement that's inside a for loop. Each iteration of the foreach statement body in Process creates a call to the Power iterator function. Each call to the iterator function proceeds to the next execution of the yield return statement, which occurs during the next iteration of the for loop.";

            Regex matchWord = new Regex(@"[^\W\d]+");
            foreach (var word in matchWord.Match(Text).AsEnumerable())
            {
                Console.WriteLine(word);
            }
        }
    }
}
