namespace ProcessingTools.Harvesters.Models.Meta
{
    using ProcessingTools.Contracts.Models.Harvesters.Meta;

    public class PersonNameModel : IPersonNameModel
    {
        public string GivenNames { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public string Surname { get; set; }
    }
}
