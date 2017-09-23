namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface IAddressable : IServiceModel
    {
        ICollection<IAddress> Addresses { get; }
    }
}
