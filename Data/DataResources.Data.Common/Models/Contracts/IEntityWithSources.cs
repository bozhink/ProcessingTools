namespace ProcessingTools.DataResources.Data.Common.Models.Contracts
{
    using System.Collections.Generic;

    public interface IEntityWithSources
    {
        ICollection<ISourceIdEntity> Sources { get; }
    }
}
