// <copyright file="BioDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioDataInitializer : DbContextInitializer<BioDbContext>, IBioDataInitializer
    {
        public BioDataInitializer(BioDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
