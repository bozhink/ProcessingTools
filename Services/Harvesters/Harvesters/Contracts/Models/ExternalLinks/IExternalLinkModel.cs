namespace ProcessingTools.Harvesters.Contracts.Models.ExternalLinks
{
    public interface IExternalLinkModel
    {
        string BaseAddress { get; }

        string Uri { get; }

        string Value { get; }
    }
}
