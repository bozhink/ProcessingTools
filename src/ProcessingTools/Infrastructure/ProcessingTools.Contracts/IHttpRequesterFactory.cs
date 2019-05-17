// <copyright file="IHttpRequesterFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    /// <summary>
    /// Factory for <see cref="IHttpRequester"/> objects.
    /// </summary>
    public interface IHttpRequesterFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="IHttpRequester"/>.
        /// </summary>
        /// <returns>Instance of <see cref="IHttpRequester"/>.</returns>
        IHttpRequester Create();

        /// <summary>
        /// Creates an instance of <see cref="IHttpRequester"/> with specified base address.
        /// </summary>
        /// <param name="baseAddress">Base address for the URL.</param>
        /// <returns>Instance of <see cref="IHttpRequester"/>.</returns>
        IHttpRequester Create(string baseAddress);
    }
}
