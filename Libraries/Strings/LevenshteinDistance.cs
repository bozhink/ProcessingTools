namespace ProcessingTools.Strings
{
    using System;

    /// <summary>
    /// Contains approximate string matching
    /// </summary>
    public class LevenshteinDistance
    {
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int Compute(string string1, string string2)
        {
            int string1Length = string1.Length;
            int string2Length = string2.Length;
            var distanceMatrix = new int[string1Length + 1, string2Length + 1];

            // Step 1
            if (string1Length == 0)
            {
                return string2Length;
            }

            if (string2Length == 0)
            {
                return string1Length;
            }

            // Step 2
            for (int i = 0; i <= string1Length; distanceMatrix[i, 0] = i++)
            {
            }

            for (int j = 0; j <= string2Length; distanceMatrix[0, j] = j++)
            {
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
    }
}
