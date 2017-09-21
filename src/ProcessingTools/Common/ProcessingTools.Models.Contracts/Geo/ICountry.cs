namespace ProcessingTools.Contracts.Models.Geo
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface ICountry : IGeoSynonymisable<ICountrySynonym>, INameableIntegerIdentifiable, IAbbreviatedNameable, IServiceModel
    {
        string CallingCode { get; }

        string LanguageCode { get; }

        string Iso639xCode { get; }

        ICollection<IContinent> Continents { get; }

        ICollection<IState> States { get; }

        ICollection<IProvince> Provinces { get; }

        ICollection<IRegion> Regions { get; }

        ICollection<IDistrict> Districts { get; }

        ICollection<IMunicipality> Municipalities { get; }

        ICollection<ICounty> Counties { get; }

        ICollection<ICity> Cities { get; }
    }
}
