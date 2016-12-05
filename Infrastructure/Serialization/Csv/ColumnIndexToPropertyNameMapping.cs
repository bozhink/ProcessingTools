namespace ProcessingTools.Serialization.Csv
{
    using System.Collections.Generic;

    /// <summary>
    /// This general class handles mapping columns to object’s properties.
    /// </summary>
    public class ColumnIndexToPropertyNameMapping
    {
        /// <summary>
        /// Initializes new object with empty Mapping dictionary.
        /// </summary>
        public ColumnIndexToPropertyNameMapping()
        {
            this.Mapping = new Dictionary<string, int>();
        }

        /// <summary>
        /// A dictionary holding Property Names (Key) and column indexes (Value).
        /// </summary>
        /// <remarks>
        /// Indexes should be 0-based.
        /// </remarks>
        public IDictionary<string, int> Mapping { get; set; }
    }
}
