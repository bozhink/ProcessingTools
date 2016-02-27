namespace ProcessingTools.Harvesters.Models.Contracts
{
    public interface IAbbreviationModel
    {
        string ContentType { get; set; }

        string Definition { get; set; }

        string Value { get; set; }
    }
}