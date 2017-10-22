namespace ProcessingTools.Bio.Services.Data.Contracts
{
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts.Data;

    public interface IMorphologicalEpithetsDataService : IMultiDataServiceAsync<MorphologicalEpithetServiceModel, IFilter>
    {
    }
}
