namespace ProcessingTools.Documents.Services.Data.Models.Publishers.Contracts
{
    using ProcessingTools.Contracts;

    public interface IPublisherJournal : IGuidIdentifiable
    {
        string Name { get; }
    }
}
