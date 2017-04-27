namespace ProcessingTools.Data.Common.Entity.Models.Contracts
{
    using System.Collections.Generic;

    public interface IEntityWithPreJoinedFields
    {
        IEnumerable<string> PreJoinFieldNames { get; }
    }
}
