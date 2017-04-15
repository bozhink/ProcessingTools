﻿namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface IRegion : ISynonymisable<IRegionSynonym>, INameableIntegerIdentifiable, IServiceModel
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