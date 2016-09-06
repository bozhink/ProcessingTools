namespace ProcessingTools.Documents.Services.Data.Models.Publishers.Contracts
{
    using System.Collections.Generic;

    public interface IPublisherDetailedServiceModel : IPublisherSimpleServiceModel
    {
        ICollection<IPublisherAddress> Addresses { get; }

        ICollection<IPublisherJournal> Journals { get; }
    }
}
