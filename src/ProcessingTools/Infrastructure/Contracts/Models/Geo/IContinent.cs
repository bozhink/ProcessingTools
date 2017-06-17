namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IContinent : IGeoSynonymisable<IContinentSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        ICollection<ICountry> Countries { get; }
    }
}
