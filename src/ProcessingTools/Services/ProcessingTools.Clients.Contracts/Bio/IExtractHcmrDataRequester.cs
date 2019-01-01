﻿// <copyright file="IExtractHcmrDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.Bio
{
    using ProcessingTools.Clients.Models.Bio.ExtractHcmr.Xml;
    using ProcessingTools.Contracts;

    /// <summary>
    /// EXTRACT HCMR data requester.
    /// </summary>
    public interface IExtractHcmrDataRequester : IDataRequester<ExtractHcmrResponseModel>
    {
    }
}
