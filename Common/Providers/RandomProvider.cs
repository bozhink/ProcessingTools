namespace ProcessingTools.Common.Providers
{
    using System;
    using Contracts;

    public class RandomProvider : IRandomProvider
    {
        private static readonly Random Random = new Random();

        public int GetRandomNumber(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
    }
}