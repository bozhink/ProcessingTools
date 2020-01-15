// <copyright file="BioEnvironmentsDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioEnvironmentsDataInitializer : DbContextInitializer<BioEnvironmentsDbContext>, IBioEnvironmentsDataInitializer
    {
        public BioEnvironmentsDataInitializer(BioEnvironmentsDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
