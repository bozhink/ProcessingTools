namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;

    public interface IGeoSynonymisable<T>
        where T : IGeoSynonym
    {
        ICollection<T> Synonyms { get; }
    }
}
