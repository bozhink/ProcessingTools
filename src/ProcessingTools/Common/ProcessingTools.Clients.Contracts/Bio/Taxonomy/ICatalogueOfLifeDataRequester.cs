// <copyright file="ICatalogueOfLifeDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Catalogue of Life (CoL) data requester.
    /// </summary>
    public interface ICatalogueOfLifeDataRequester : IDataRequester<CatalogueOfLifeApiServiceResponseModel>
    {
    }
}
