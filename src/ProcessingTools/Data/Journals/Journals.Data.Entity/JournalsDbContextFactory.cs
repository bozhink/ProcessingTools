﻿namespace ProcessingTools.Journals.Data.Entity
{
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Journals.Data.Entity.Contracts;

    public class JournalsDbContextFactory : IJournalsDbContextFactory
    {
        public JournalsDbContext Create()
        {
            return new JournalsDbContext(ConnectionStrings.JournalsDatabaseConnection);
        }
    }
}
