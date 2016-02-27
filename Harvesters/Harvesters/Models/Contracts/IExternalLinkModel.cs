namespace ProcessingTools.Harvesters.Models.Contracts
{
    public interface IExternalLinkModel
    {
        string BaseAddress { get; set; }

        string Uri { get; set; }

        string Value { get; set; }
    }
}