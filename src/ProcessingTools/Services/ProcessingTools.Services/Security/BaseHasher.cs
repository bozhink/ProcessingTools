// <copyright file="BaseHasher.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Security
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using ProcessingTools.Contracts.Services;

    /// <summary>
    /// Hasher base class.
    /// </summary>
    public class BaseHasher : IHasher, IDisposable
    {
        private readonly HashAlgorithm hashAlgorithm;
        private readonly Encoding encoding;

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseHasher"/> class.
        /// </summary>
        /// <param name="hashAlgorithm">Hash algorithm to be applied.</param>
        /// <param name="encoding">Encoding of the string values.</param>
        public BaseHasher(HashAlgorithm hashAlgorithm, Encoding encoding)
        {
            this.hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException(nameof(hashAlgorithm));
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseHasher"/> class.
        /// </summary>
        ~BaseHasher()
        {
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public string ComputeHash(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            byte[] bytes = this.encoding.GetBytes(source);
            byte[] hash = this.hashAlgorithm.ComputeHash(bytes);

            StringBuilder stringBuilder = new StringBuilder(hash.Length);

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2", CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }

        /// <inheritdoc/>
        public string ComputeHashBase64(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return null;
            }

            byte[] bytes = this.encoding.GetBytes(source);
            byte[] hash = this.hashAlgorithm.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            this.Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value that indicates whether the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any managed objects here.
                this.hashAlgorithm.Dispose();
            }

            this.disposed = true;
        }
    }
}
