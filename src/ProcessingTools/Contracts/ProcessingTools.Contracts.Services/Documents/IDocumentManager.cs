// <copyright file="IDocumentManager.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    /// <summary>
    /// Document manager.
    /// </summary>
    public interface IDocumentManager : IReadDocumentHelper, IWriteDocumentHelper
    {
    }
}
