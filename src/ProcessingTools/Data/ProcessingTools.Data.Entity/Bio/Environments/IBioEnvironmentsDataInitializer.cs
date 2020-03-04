// <copyright file="IBioEnvironmentsDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    /// <summary>
    /// Bio environments database initializer.
    /// </summary>
    public interface IBioEnvironmentsDataInitializer : IDbContextInitializer<BioEnvironmentsDbContext>
    {
    }
}
