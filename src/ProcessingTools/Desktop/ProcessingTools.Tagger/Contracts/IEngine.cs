// <copyright file="IEngine.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Contracts
{
    /// <summary>
    /// Engine.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Run the execution of the engine.
        /// </summary>
        /// <param name="args">Arguments for the run.</param>
        void Run(string[] args);
    }
}
