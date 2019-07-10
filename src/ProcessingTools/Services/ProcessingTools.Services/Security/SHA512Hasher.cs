// <copyright file="SHA512Hasher.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Security.Cryptography;
using ProcessingTools.Common.Constants;

#pragma warning disable S101 // Types should be named in PascalCase

namespace ProcessingTools.Services.Security
{
    /// <summary>
    /// SHA512 hasher.
    /// </summary>
    public class SHA512Hasher : BaseHasher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SHA512Hasher"/> class.
        /// </summary>
        public SHA512Hasher()
            : base(SHA512.Create(), Defaults.Encoding)
        {
        }
    }
}

#pragma warning restore S101 // Types should be named in PascalCase
