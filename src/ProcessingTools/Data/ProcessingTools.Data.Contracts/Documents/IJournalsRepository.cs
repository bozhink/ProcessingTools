﻿// <copyright file="IJournalsRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journals repository.
    /// </summary>
    public interface IJournalsRepository : ICrudRepository<IJournal>
    {
    }
}
