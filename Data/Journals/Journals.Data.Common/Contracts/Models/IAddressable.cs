namespace ProcessingTools.Journals.Data.Common.Contracts.Models
{
    using System.Collections.Generic;

    public interface IAddressable
    {
        ICollection<IAddress> Addresses { get; }
    }
}
