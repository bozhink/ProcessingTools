namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface ICounty : IGeoSynonymisable<ICountySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        int CountryId { get; }

        int? StateId { get; }

        int? ProvinceId { get; }

        int? RegionId { get; }

        int? DistrictId { get; }

        int? MunicipalityId { get; }

        ICollection<ICity> Cities { get; }
    }
}
