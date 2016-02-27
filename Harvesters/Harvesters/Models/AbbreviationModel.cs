namespace ProcessingTools.Harvesters.Models
{
    using Contracts;

    public class AbbreviationModel : IAbbreviationModel
    {
        public string Definition { get; set; }

        public string Value { get; set; }
    }
}