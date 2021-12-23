// <copyright file="DisposableExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;

    /// <summary>
    /// Extensions related to <see cref="IDisposable"/>.
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// Checks if the object is IDisposable, and if it is tries to dispose it.
        /// </summary>
        /// <param name="instance">Object instance to be disposed.</param>
        /// <returns>DisposeStatus. On exception returns NotDisposed.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Expected")]
        public static DisposeStatus TryDispose(this object instance)
        {
            if (instance is IDisposable)
            {
                try
                {
                    (instance as IDisposable).Dispose();
                    return DisposeStatus.Disposed;
                }
                catch
                {
                    return DisposeStatus.NotDisposed;
                }
            }

            return DisposeStatus.NotDisposable;
        }
    }
}
