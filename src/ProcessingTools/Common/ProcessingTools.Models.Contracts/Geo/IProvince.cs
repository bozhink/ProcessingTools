namespace ProcessingTools.Models.Contracts.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts;

    public interface IProvince : IGeoSynonymisable<IProvinceSynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        int CountryId { get; }

        int? StateId { get; }

        ICollection<IRegion> Regions { get; }

        ICollection<IDistrict> Districts { get; }

        ICollection<IMunicipality> Municipalities { get; }

        ICollection<ICounty> Counties { get; }

        ICollection<ICity> Cities { get; }
    }
}
