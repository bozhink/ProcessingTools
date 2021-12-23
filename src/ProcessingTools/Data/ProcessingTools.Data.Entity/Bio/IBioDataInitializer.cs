// <copyright file="IBioDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IBioDataInitializer : IDbContextInitializer<BioDbContext>
    {
    }
}
