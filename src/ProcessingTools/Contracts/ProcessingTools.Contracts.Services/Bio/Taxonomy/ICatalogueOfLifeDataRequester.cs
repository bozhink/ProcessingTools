// <copyright file="ICatalogueOfLifeDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife;

    /// <summary>
    /// Catalogue of Life (CoL) data requester.
    /// </summary>
    public interface ICatalogueOfLifeDataRequester : IDataRequester<CatalogueOfLifeApiServiceXmlResponseModel>
    {
    }
}
