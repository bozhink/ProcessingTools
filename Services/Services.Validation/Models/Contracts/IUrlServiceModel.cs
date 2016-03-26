namespace ProcessingTools.Services.Validation.Models.Contracts
{
    public interface IUrlServiceModel
    {
        string BaseAddress { get; set; }

        string Address { get; set; }

        string FullAddress { get; }
    }
}
