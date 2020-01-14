// <copyright file="ICatalogueOfLifeWebserviceClient.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife;

    /// <summary>
    /// Catalogue of Life (CoL) webservice client.
    /// </summary>
    public interface ICatalogueOfLifeWebserviceClient
    {
        /// <summary>
        /// Search scientific name in Catalogue Of Life (CoL).
        /// </summary>
        /// <param name="name">Scientific name of the taxon which rank is searched.</param>
        /// <returns><see cref="CatalogueOfLifeApiServiceXmlResponseModel"/> of the CoL Webservice response.</returns>
        Task<CatalogueOfLifeApiServiceXmlResponseModel> GetDataPerNameAsync(string name);
    }
}
