// <copyright file="SHA384Hasher.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

#pragma warning disable S101 // Types should be named in PascalCase

namespace ProcessingTools.Security
{
    using System.Security.Cryptography;
    using ProcessingTools.Common.Constants;

    /// <summary>
    /// SHA384 hasher.
    /// </summary>
    public class SHA384Hasher : BaseHasher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SHA384Hasher"/> class.
        /// </summary>
        public SHA384Hasher()
            : base(SHA384.Create(), Defaults.Encoding)
        {
        }
    }
}

#pragma warning restore S101 // Types should be named in PascalCase
