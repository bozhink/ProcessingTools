namespace ProcessingTools.Resources.Data.Common.Contracts.Models
{
    using System.Collections.Generic;

    public interface IEntityWithSources
    {
        ICollection<ISourceIdEntity> Sources { get; }
    }
}
