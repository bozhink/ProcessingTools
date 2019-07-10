// <copyright file="IExtractHcmrDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml;

namespace ProcessingTools.Contracts.Services.Bio
{
    /// <summary>
    /// EXTRACT HCMR data requester.
    /// </summary>
    public interface IExtractHcmrDataRequester : IDataRequester<ExtractHcmrResponseModel>
    {
    }
}
