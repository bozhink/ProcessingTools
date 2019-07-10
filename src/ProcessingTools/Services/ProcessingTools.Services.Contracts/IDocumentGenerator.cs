﻿// <copyright file="IDocumentGenerator.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Models;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Document generator.
    /// </summary>
    public interface IDocumentGenerator : IGenerator<IDocument, object>
    {
    }
}
