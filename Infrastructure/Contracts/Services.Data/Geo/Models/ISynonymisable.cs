namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using System.Collections.Generic;

    public interface ISynonymisable<T>
        where T : ISynonym
    {
        ICollection<T> Synonyms { get; }
    }
}
