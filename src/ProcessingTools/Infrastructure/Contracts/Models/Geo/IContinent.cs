namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IContinent : IGeoSynonymisable<IContinentSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        ICollection<ICountry> Countries { get; }
    }
}
