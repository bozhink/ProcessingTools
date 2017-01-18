namespace ProcessingTools.Harvesters.Contracts.Models.Meta
{
    public interface IAuthor
    {
        string GivenNames { get; }

        string Prefix { get; }

        string Suffix { get; }

        string Surname { get; }
    }
}
