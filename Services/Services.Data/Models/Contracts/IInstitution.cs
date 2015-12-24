namespace ProcessingTools.Services.Data.Models.Contracts
{
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface IInstitution : IDataServiceModel
    {
        string Name { get; set; }
    }
}