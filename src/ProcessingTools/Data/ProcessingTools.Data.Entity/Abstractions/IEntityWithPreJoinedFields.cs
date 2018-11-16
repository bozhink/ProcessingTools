namespace ProcessingTools.Data.Entity.Abstractions
{
    using System.Collections.Generic;

    public interface IEntityWithPreJoinedFields
    {
        IEnumerable<string> PreJoinFieldNames { get; }
    }
}
