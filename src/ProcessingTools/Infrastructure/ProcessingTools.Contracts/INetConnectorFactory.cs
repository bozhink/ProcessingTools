// <copyright file="INetConnectorFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    /// <summary>
    /// Factory for <see cref="INetConnector"/> objects.
    /// </summary>
    public interface INetConnectorFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="INetConnector"/>.
        /// </summary>
        /// <returns>Instance of <see cref="INetConnector"/></returns>
        INetConnector Create();

        /// <summary>
        /// Creates an instance of <see cref="INetConnector"/> with specified base address.
        /// </summary>
        /// <param name="baseAddress">Base address for the URL</param>
        /// <returns>Instance of <see cref="INetConnector"/></returns>
        INetConnector Create(string baseAddress);
    }
}
