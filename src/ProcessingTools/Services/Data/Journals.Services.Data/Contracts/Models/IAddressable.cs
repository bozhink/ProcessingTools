namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IAddressable : IServiceModel
    {
        ICollection<IAddress> Addresses { get; }
    }
}
