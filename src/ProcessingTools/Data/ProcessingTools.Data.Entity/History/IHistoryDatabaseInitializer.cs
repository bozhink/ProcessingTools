// <copyright file="IHistoryDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.History
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IHistoryDatabaseInitializer : IDbContextInitializer<HistoryDbContext>
    {
    }
}
