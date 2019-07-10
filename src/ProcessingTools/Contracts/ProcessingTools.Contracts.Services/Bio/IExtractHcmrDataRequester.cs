// <copyright file="IExtractHcmrDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio
{
    using ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml;

    /// <summary>
    /// EXTRACT HCMR data requester.
    /// </summary>
    public interface IExtractHcmrDataRequester : IDataRequester<ExtractHcmrResponseModel>
    {
    }
}
