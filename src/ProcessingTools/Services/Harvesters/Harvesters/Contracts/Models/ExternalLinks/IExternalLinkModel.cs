namespace ProcessingTools.Harvesters.Contracts.Models.ExternalLinks
{
    using ProcessingTools.Models.Contracts;

    public interface IExternalLinkModel : IValuable, IFullAddressable
    {
        string BaseAddress { get; }

        string Uri { get; }
    }
}
