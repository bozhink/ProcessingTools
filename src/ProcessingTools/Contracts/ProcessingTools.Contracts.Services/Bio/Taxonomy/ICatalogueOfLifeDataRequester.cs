// <copyright file="ICatalogueOfLifeDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    /// <summary>
    /// Catalogue of Life (CoL) data requester.
    /// </summary>
    public interface ICatalogueOfLifeDataRequester : IDataRequester<CatalogueOfLifeApiServiceResponseModel>
    {
    }
}
