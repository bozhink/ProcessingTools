namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using System.Collections.Generic;

    public interface IGeoSynonymisable<T>
        where T : IGeoSynonym
    {
        ICollection<T> Synonyms { get; }
    }
}
