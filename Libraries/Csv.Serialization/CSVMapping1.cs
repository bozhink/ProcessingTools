namespace ProcessingTools.Csv.Serialization
{
    using System.Collections.Generic;

    //This general class handles mapping CSV to objects
    public class CSVMapping
    {
        //A dictionary holding Property Names (Key) and CSV indexes (Value)
        //0 Based index
        public IDictionary<string, int> Mapping { get; set; }
    }
}