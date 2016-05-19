namespace ProcessingTools.Common.Providers
{
    using System;
    using System.Text;

    using Contracts;

    public class RandomProvider : IRandomProvider
    {
        private const string StringCharacters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890-=[]';,./~!@#$%^&*()_+ ";

        private const string LoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam,
quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo
consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse
cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non
proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        private static readonly Random Random = new Random();

        private readonly int loremIpsumLength = LoremIpsum.Length;

        public int GetRandomNumber(int min, int max)
        {
            return Random.Next(min, max + 1);
        }

        public string GetRandomString(int length)
        {
            char[] chars = StringCharacters.ToCharArray();
            int n = chars.Length;

            var builder = new StringBuilder();
            for (int i = 0; i < length; ++i)
            {
                builder.Append(chars[Random.Next(0, n)]);
            }

            return builder.ToString();
        }

        public string GetRandomString(int minLength, int maxLength)
        {
            return this.GetRandomString(this.GetRandomNumber(minLength, maxLength));
        }

        public string GetRandomText(params string[] phrases)
        {
            var builder = new StringBuilder();
            for (int i = 0, len = phrases.Length; i < len; ++i)
            {
                builder.Append(LoremIpsum.Substring(0, this.GetRandomNumber(0, this.loremIpsumLength)));
                builder.Append(" ");
                ////builder.Append(phrases[this.GetRandomNumber(0, len)]);
                builder.Append(phrases[i]);
                builder.Append(" ");
            }

            return builder.ToString();
        }
    }
}