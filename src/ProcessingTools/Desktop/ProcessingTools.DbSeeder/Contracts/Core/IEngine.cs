// <copyright file="IEngine.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Contracts.Core
{
    using System.Threading.Tasks;

    public interface IEngine
    {
        Task RunAsync(string[] args);
    }
}
