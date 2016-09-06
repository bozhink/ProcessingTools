namespace ProcessingTools.Harvesters.Models.Contracts
{
    public interface IAbbreviationModel
    {
        string ContentType { get; }

        string Definition { get; }

        string Value { get; }
    }
}
