namespace ProcessingTools.Harvesters.Contracts.Models.ExternalLinks
{
    using ProcessingTools.Contracts.Models;

    public interface IExternalLinkModel : IValuable, IFullAddressable
    {
        string BaseAddress { get; }

        string Uri { get; }
    }
}
