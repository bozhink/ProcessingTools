namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IState : IGeoSynonymisable<IStateSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        int CountryId { get; }

        ICollection<IProvince> Provinces { get; }

        ICollection<IRegion> Regions { get; }

        ICollection<IDistrict> Districts { get; }

        ICollection<IMunicipality> Municipalities { get; }

        ICollection<ICounty> Counties { get; }

        ICollection<ICity> Cities { get; }
    }
}
