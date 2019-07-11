// <copyright file="SHA256Hasher.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

#pragma warning disable S101 // Types should be named in PascalCase

namespace ProcessingTools.Services.Security
{
    using System.Security.Cryptography;
    using ProcessingTools.Common.Constants;

    /// <summary>
    /// SHA256 hasher.
    /// </summary>
    public class SHA256Hasher : BaseHasher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SHA256Hasher"/> class.
        /// </summary>
        public SHA256Hasher()
            : base(SHA256.Create(), Defaults.Encoding)
        {
        }
    }
}

#pragma warning restore S101 // Types should be named in PascalCase
