namespace ProcessingTools.Journals.Services.Data.Contracts.Models
{
    using System.Collections.Generic;

    public interface IAddressable
    {
        ICollection<IAddress> Addresses { get; }
    }
}
