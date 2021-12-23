// <copyright file="PocoInstanceBuilder{T}.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    /// <summary>
    /// Builder for POCO instances of specified type.
    /// </summary>
    /// <typeparam name="T">Type of instance objects.</typeparam>
    public class PocoInstanceBuilder<T> : PocoInstanceBuilder
        where T : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PocoInstanceBuilder{T}"/> class.
        /// </summary>
        public PocoInstanceBuilder()
            : base(typeof(T))
        {
        }
    }
}
