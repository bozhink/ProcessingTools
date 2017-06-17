namespace ProcessingTools.Bio.Services.Data.Contracts
{
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Services.Data;

    public interface IMorphologicalEpithetsDataService : IMultiDataServiceAsync<MorphologicalEpithetServiceModel, IFilter>
    {
    }
}
