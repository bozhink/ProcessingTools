// <copyright file="IDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Contracts.Seeders
{
    using System.Threading.Tasks;

    public interface IDbSeeder
    {
        Task Seed();
    }
}
