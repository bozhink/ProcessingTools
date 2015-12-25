namespace ProcessingTools.Bio.Services.Data.Models.Contracts
{
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface IMorphologicalEpithet : IDataServiceModel
    {
        string Name { get; set; }
    }
}