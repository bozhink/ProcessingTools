// <copyright file="IMediatypesDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Files
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IMediatypesDataInitializer : IDbContextInitializer<MediatypesDbContext>
    {
    }
}
