namespace ProcessingTools.Documents.Services.Data.Models.Publishers.Contracts
{
    using System.Collections.Generic;
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface IPublisherAddableServiceModel : IAddableServiceModel, IPublisherUpdatableServiceModel
    {
        ICollection<IPublisherAddress> Addresses { get; }
    }
}
