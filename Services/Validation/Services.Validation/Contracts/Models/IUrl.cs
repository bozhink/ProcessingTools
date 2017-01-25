namespace ProcessingTools.Services.Validation.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IUrl : IAddressable
    {
        string BaseAddress { get; }

        string FullAddress { get; }
    }
}
