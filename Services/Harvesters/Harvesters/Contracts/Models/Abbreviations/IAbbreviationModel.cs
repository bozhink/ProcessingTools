namespace ProcessingTools.Harvesters.Contracts.Models.Abbreviations
{
    public interface IAbbreviationModel
    {
        string ContentType { get; }

        string Definition { get; }

        string Value { get; }
    }
}
