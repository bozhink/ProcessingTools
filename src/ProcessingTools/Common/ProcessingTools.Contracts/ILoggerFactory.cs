// <copyright file="ILoggerFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System;

    /// <summary>
    /// Factory for loggers.
    /// </summary>
    public interface ILoggerFactory : IFactory<ILogger>
    {
        /// <summary>
        /// Creates a logger with category of specified type.
        /// </summary>
        /// <param name="type">Type of object for which logger will log</param>
        /// <returns><see cref="ILogger"/> object</returns>
        ILogger Create(Type type);
    }
}
