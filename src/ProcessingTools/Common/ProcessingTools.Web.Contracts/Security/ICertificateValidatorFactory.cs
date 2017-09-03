// <copyright file="ICertificateValidatorFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Contracts.Security
{
    using Microsoft.Owin.Security;

    /// <summary>
    /// Factory for <see cref="ICertificateValidator"/> instances
    /// </summary>
    public interface ICertificateValidatorFactory
    {
        /// <summary>
        /// Creates instance of <see cref="ICertificateValidator"/>.
        /// </summary>
        /// <returns>Instance of <see cref="ICertificateValidator"/></returns>
        ICertificateValidator Create();
    }
}
