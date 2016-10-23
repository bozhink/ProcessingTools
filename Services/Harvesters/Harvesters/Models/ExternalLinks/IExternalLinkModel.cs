namespace ProcessingTools.Harvesters.Models.ExternalLinks
{
    public interface IExternalLinkModel
    {
        string BaseAddress { get; }

        string Uri { get; }

        string Value { get; }
    }
}
