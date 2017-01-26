namespace ProcessingTools.Services.Validation.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IUrl : IAddressable, IFullAddressable, IPermalinkable
    {
        string BaseAddress { get; }
    }
}
