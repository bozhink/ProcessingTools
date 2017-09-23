namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface IRegion : IGeoSynonymisable<IRegionSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        int CountryId { get; }

        int? StateId { get; }

        int? ProvinceId { get; }

        ICollection<IDistrict> Districts { get; }

        ICollection<IMunicipality> Municipalities { get; }

        ICollection<ICounty> Counties { get; }

        ICollection<ICity> Cities { get; }
    }
}
