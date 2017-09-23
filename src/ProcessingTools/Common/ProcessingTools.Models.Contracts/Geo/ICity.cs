namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface ICity : IGeoSynonymisable<ICitySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        int CountryId { get; }

        ICountry Country { get; }

        int? StateId { get; }

        int? ProvinceId { get; }

        int? RegionId { get; }

        int? DistrictId { get; }

        int? MunicipalityId { get; }

        int? CountyId { get; }

        ICollection<IPostCode> PostCodes { get; }
    }
}
