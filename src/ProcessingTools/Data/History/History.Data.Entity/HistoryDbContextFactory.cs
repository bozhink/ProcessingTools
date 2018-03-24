﻿namespace ProcessingTools.History.Data.Entity.Factories
{
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.History.Data.Entity.Contracts;

    public class HistoryDbContextFactory : IHistoryDbContextFactory
    {
        public HistoryDbContext Create()
        {
            return new HistoryDbContext(ConnectionStrings.HistoryDatabaseConnection);
        }
    }
}
