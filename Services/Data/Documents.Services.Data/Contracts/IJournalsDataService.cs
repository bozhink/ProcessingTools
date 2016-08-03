namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using Models.Journals;
    using ProcessingTools.Services.Common.Contracts;

    public interface IJournalsDataService : IMvcDataService<JournalMinimalServiceModel, JournalServiceModel, JournalDetailsServiceModel>
    {
    }
}
