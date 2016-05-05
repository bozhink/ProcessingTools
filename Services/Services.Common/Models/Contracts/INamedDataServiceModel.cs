namespace ProcessingTools.Services.Common.Models.Contracts
{
    public interface INamedDataServiceModel : ISimpleServiceModel
    {
        string Name { get; set; }
    }
}
