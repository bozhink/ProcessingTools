namespace ProcessingTools.Contracts.Data.DataResources.Models
{
    using System.Collections.Generic;

    public interface IEntityWithSources
    {
        ICollection<ISourceIdEntity> Sources { get; }
    }
}
