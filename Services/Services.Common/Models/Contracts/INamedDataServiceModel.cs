namespace ProcessingTools.Services.Common.Models.Contracts
{
    public interface INamedDataServiceModel : IDataServiceModel
    {
        string Name { get; set; }
    }
}
