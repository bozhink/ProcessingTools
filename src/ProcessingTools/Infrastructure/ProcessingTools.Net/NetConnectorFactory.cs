// <copyright file="NetConnectorFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Net
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Factory for <see cref="INetConnector"/> objects.
    /// </summary>
    public class NetConnectorFactory : INetConnectorFactory
    {
        /// <summary>
        /// Creates instance of <see cref="INetConnector"/> with default parameters.
        /// </summary>
        /// <returns>Instance of <see cref="INetConnector"/>.</returns>
        public INetConnector Create() => new NetConnector();

        /// <summary>
        /// Creates instance of <see cref="INetConnector"/> with specified base address..
        /// </summary>
        /// <param name="baseAddress">Base address for the requested API.</param>
        /// <returns>Instance of <see cref="INetConnector"/>.</returns>
        public INetConnector Create(string baseAddress) => new NetConnector(baseAddress);
    }
}
