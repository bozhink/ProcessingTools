// <copyright file="IBioTaxonomyDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Taxonomy
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IBioTaxonomyDataInitializer : IDbContextInitializer<BioTaxonomyDbContext>
    {
    }
}
