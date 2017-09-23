namespace ProcessingTools.Contracts.Data.Journals.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface IAddressable : IDataModel
    {
        IEnumerable<IAddress> Addresses { get; }
    }
}
