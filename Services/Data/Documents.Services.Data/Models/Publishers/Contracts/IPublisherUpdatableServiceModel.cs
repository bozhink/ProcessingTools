namespace ProcessingTools.Documents.Services.Data.Models.Publishers.Contracts
{
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface IPublisherUpdatableServiceModel : IUpdatableServiceModel, IPublisherListableServiceModel
    {
        string AbbreviatedName { get; }
    }
}
