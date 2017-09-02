namespace ProcessingTools.Contracts.Data.DataResources.Models
{
    using System.Collections.Generic;

    public interface IEntityWithSources
    {
        IEnumerable<ISourceIdEntity> Sources { get; }
    }
}
