namespace ProcessingTools.Data.Models.Entity.Geo
{
    using System.Collections.Generic;

    public interface ISynonymisable<T>
        where T : ISynonym
    {
        ICollection<T> Synonyms { get; }
    }
}
