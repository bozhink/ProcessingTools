namespace ProcessingTools.Geo.Services.Data.Models.Contracts
{
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface IGeoName : IDataServiceModel
    {
        string Name { get; set; }
    }
}