namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using System.Collections.Generic;

    public interface IAddressableEntity
    {
        IEnumerable<IAddressEntity> Addresses { get; }
    }
}
