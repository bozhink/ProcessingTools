namespace ProcessingTools.Contracts
{
    public interface IPerson
    {
        string GivenNames { get; }

        string Prefix { get; }

        string Suffix { get; }

        string Surname { get; }
    }
}
