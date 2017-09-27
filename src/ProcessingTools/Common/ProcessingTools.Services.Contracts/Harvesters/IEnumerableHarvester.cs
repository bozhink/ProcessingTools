// <copyright file="IEnumerableHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters
{
    using System.Collections.Generic;

    public interface IEnumerableHarvester<TContext, TResult> : IHarvester<TContext, IEnumerable<TResult>>
    {
    }
}
