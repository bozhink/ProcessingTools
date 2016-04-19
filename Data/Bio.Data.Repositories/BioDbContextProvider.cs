﻿namespace ProcessingTools.Bio.Data.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Bio.Data.Contracts;

    public class BioDbContextProvider : IBioDbContextProvider
    {
        private readonly IBioDbContextFactory contextFactory;

        public BioDbContextProvider(IBioDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public BioDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}