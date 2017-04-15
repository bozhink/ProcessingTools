namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IContinent : ISynonymisable<IContinentSynonym>, INameableIntegerIdentifiable, IServiceModel
    {
        ICollection<ICountry> Countries { get; }
    }
}
