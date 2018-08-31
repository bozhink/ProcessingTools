// <copyright file="IRandomDataProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    /// <summary>
    /// Provider of random data.
    /// </summary>
    public interface IRandomDataProvider
    {
        /// <summary>
        /// Gets a random integer in specified interval.
        /// </summary>
        /// <param name="min">Minimal value of the random integer</param>
        /// <param name="max">Maximal value of the random integer</param>
        /// <returns>Random integer</returns>
        int GetRandomNumber(int min, int max);

        /// <summary>
        /// Gets random string with specified length.
        /// </summary>
        /// <param name="length">Length of the resultant string.</param>
        /// <returns>Random string</returns>
        string GetRandomString(int length);

        /// <summary>
        /// Gets random string with variable length in specified interval.
        /// </summary>
        /// <param name="minLength">Minimal length of the resultant string</param>
        /// <param name="maxLength">Maximal length of the resultant string</param>
        /// <returns>Random string</returns>
        string GetRandomString(int minLength, int maxLength);

        /// <summary>
        /// Get random text based on specified phrases.
        /// </summary>
        /// <param name="phrases">Phrased to be used for resultant random string construction.</param>
        /// <returns>Random string</returns>
        string GetRandomText(params string[] phrases);
    }
}
