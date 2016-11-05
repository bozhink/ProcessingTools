namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using System.Collections.Generic;

    public interface IAddressableEntity
    {
        ICollection<IAddressEntity> Addresses { get; }
    }
}
