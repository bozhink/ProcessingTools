namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface IMunicipality : IGeoSynonymisable<IMunicipalitySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        int CountryId { get; }

        int? StateId { get; }

        int? ProvinceId { get; }

        int? RegionId { get; }

        int? DistrictId { get; }

        ICollection<ICounty> Counties { get; }

        ICollection<ICity> Cities { get; }
    }
}
