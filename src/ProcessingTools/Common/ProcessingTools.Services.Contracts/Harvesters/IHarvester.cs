// <copyright file="IHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters
{
    using System.Threading.Tasks;

    public interface IHarvester<TContext, TResult>
    {
        Task<TResult> Harvest(TContext context);
    }
}
