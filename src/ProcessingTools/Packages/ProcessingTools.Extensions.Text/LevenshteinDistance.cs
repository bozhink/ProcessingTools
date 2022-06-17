// <copyright file="LevenshteinDistance.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

// See https://www.datacamp.com/community/tutorials/fuzzy-string-python
namespace ProcessingTools.Extensions.Text
{
    using System;

    /// <summary>
    /// Contains approximate string matching.
    /// </summary>
    public static class LevenshteinDistance
    {
        /// <summary>
        /// Default Fuzziness Radius.
        /// </summary>
        public const int DefaultFuzzinessRadius = 3;

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        /// <param name="string1">Left string to compare.</param>
        /// <param name="string2">Right string to compare.</param>
        /// <returns>Levenstein distance.</returns>
        public static int Compute(string string1, string string2)
        {
            int string1Length = string1?.Length ?? 0;
            int string2Length = string2?.Length ?? 0;

            // Step 1
            if (string1Length == 0)
            {
                return string2Length;
            }

            if (string2Length == 0)
            {
                return string1Length;
            }

            var distanceMatrix = new int[string1Length + 1, string2Length + 1];

            // Step 2
            for (int i = 0; i <= string1Length; distanceMatrix[i, 0] = i++)
            {
                // Skip
            }

            for (int j = 0; j <= string2Length; distanceMatrix[0, j] = j++)
            {
                // Skip
            }

            // Step 3
            for (int i = 1; i <= string1Length; i++)
            {
                // Step 4
                for (int j = 1; j <= string2Length; j++)
                {
                    // Step 5
                    int cost = (string2[j - 1] == string1[i - 1]) ? 0 : 1;

                    // Step 6
                    distanceMatrix[i, j] = Math.Min(
                        Math.Min(distanceMatrix[i - 1, j] + 1, distanceMatrix[i, j - 1] + 1),
                        distanceMatrix[i - 1, j - 1] + cost);
                }
            }

            // Step 7
            return distanceMatrix[string1Length, string2Length];
        }

        /// <summary>
        /// Calculates relative similarity of two strings.
        /// </summary>
        /// <param name="string1">Left string to be compared.</param>
        /// <param name="string2">Right string to be compared.</param>
        /// <returns><see cref="SimilarityType"/> of comparison.</returns>
        public static SimilarityType ComputeSimilarity(string string1, string string2) => ComputeSimilarity(string1: string1, string2: string2, fuzzinessRadius: DefaultFuzzinessRadius);

        /// <summary>
        /// Calculates relative similarity of two strings.
        /// </summary>
        /// <param name="string1">Left string to be compared.</param>
        /// <param name="string2">Right string to be compared.</param>
        /// <param name="fuzzinessRadius">Radius of fuzziness to be used.</param>
        /// <returns><see cref="SimilarityType"/> of comparison.</returns>
        public static SimilarityType ComputeSimilarity(string string1, string string2, uint fuzzinessRadius)
        {
            int distance = Compute(string1, string2);

            if (distance < 1)
            {
                return SimilarityType.Same;
            }

            if (distance > fuzzinessRadius)
            {
                return SimilarityType.NotSimilar;
            }

            return SimilarityType.Similar;
        }

        /// <summary>
        /// Computes the similarity ratio of two strings.
        /// </summary>
        /// <param name="string1">Left string to be compared.</param>
        /// <param name="string2">Right string to be compared.</param>
        /// <returns>Double value of the similarity ratio.</returns>
        public static double ComputeSimilarityRatio(string string1, string string2)
        {
            var totalLength = (string1?.Length ?? 0) + (string2?.Length ?? 0);
            if (totalLength < 1)
            {
                return 0;
            }

            var distance = Compute(string1, string2);

            return (totalLength - distance) * 1.0 / totalLength;
        }
    }
}
