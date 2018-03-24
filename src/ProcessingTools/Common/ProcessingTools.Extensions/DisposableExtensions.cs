// <copyright file="DisposableExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Extensions related to <see cref="IDisposable"/>.
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// Checks if the object is IDisposable, and if it is tries to dispose it.
        /// </summary>
        /// <param name="obj">Object to be disposed.</param>
        /// <returns>DisposeStatus. On exception returns NotDisposed.</returns>
        public static DisposeStatus TryDispose(this object obj)
        {
            if (obj is IDisposable)
            {
                try
                {
                    (obj as IDisposable).Dispose();
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
