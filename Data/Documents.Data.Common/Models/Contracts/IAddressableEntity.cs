namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System.Collections.Generic;

    public interface IAddressableEntity
    {
        ICollection<IAddressEntity> Addresses { get; }
    }
}
