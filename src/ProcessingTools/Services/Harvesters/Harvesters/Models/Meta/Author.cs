namespace ProcessingTools.Harvesters.Models.Meta
{
    using ProcessingTools.Contracts.Models.Harvesters.Meta;

    internal class Author : IAuthor
    {
        public string GivenNames { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public string Surname { get; set; }
    }
}
