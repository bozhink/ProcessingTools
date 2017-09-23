namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface IContinent : IGeoSynonymisable<IContinentSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        ICollection<ICountry> Countries { get; }
    }
}
