// <copyright file="HttpRequesterFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Net
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Factory for <see cref="IHttpRequester"/> objects.
    /// </summary>
    public class HttpRequesterFactory : IHttpRequesterFactory
    {
        /// <summary>
        /// Creates instance of <see cref="IHttpRequester"/> with default parameters.
        /// </summary>
        /// <returns>Instance of <see cref="IHttpRequester"/>.</returns>
        public IHttpRequester Create() => new HttpRequester();

        /// <summary>
        /// Creates instance of <see cref="IHttpRequester"/> with specified base address..
        /// </summary>
        /// <param name="baseAddress">Base address for the requested API.</param>
        /// <returns>Instance of <see cref="IHttpRequester"/>.</returns>
        public IHttpRequester Create(string baseAddress) => new HttpRequester(baseAddress);
    }
}
