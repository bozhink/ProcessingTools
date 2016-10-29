namespace ProcessingTools.Processors.Models.Bio.Codes
{
    public class SpecimenCode : ISpecimenCode
    {
        public SpecimenCode(string prefix, string type, string code = null, string fullString = null)
        {
            this.Prefix = prefix;
            this.Type = type;
            this.Code = code;
            this.FullString = fullString;
        }

        public string Prefix { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }

        public string FullString { get; set; }
    }
}
