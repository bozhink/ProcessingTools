namespace ProcessingTools.Csv.Serialization
{
    using System.Collections.Generic;

    /// <summary>
    /// This general class handles mapping CSV to objects.
    /// </summary>
    public class CsvToObjectMapping
    {
        /// <summary>
        /// Initializes new object with empty Mapping dictionary.
        /// </summary>
        public CsvToObjectMapping()
        {
            this.Mapping = new Dictionary<string, int>();
        }

        /// <summary>
        /// A dictionary holding Property Names (Key) and CSV column indexes (Value).
        /// </summary>
        /// <remarks>
        /// Indexes should be 0-based.
        /// </remarks>
        public IDictionary<string, int> Mapping { get; set; }
    }
}