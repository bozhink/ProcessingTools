// <copyright file="CompressionExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

// See https://stackoverflow.com/questions/7343465/compression-decompression-string-with-c-sharp
namespace ProcessingTools.Extensions
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Compression extensions.
    /// </summary>
    public static class CompressionExtensions
    {
        /// <summary>
        /// Compress string to byte array.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <param name="encoding">Text encoding.</param>
        /// <returns>Byte array.</returns>
        public static byte[] Compress(this string source, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (source == null)
            {
                return new byte[] { };
            }

            var bytes = encoding.GetBytes(source);

            using (var inputStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    {
                        inputStream.CopyTo(gzipStream);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Compress string to byte array.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <returns>Byte array.</returns>
        public static byte[] Compress(this string source) => source.Compress(Encoding.UTF8);

        /// <summary>
        /// Compress string to byte array.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <param name="encoding">Text encoding.</param>
        /// <returns>Byte array.</returns>
        public static async Task<byte[]> CompressAsync(this string source, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (source == null)
            {
                return new byte[] { };
            }

            var bytes = encoding.GetBytes(source);

            using (var inputStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    {
                        await inputStream.CopyToAsync(gzipStream).ConfigureAwait(false);
                    }

                    return outputStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Compress string to byte array.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <returns>Byte array.</returns>
        public static Task<byte[]> CompressAsync(this string source) => source.CompressAsync(Encoding.UTF8);

        /// <summary>
        /// Compresses string to string.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <param name="encoding">Text encoding.</param>
        /// <returns>Compressed string.</returns>
        public static string CompressString(this string source, Encoding encoding)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = source.Compress(encoding);

            if (buffer == null)
            {
                return null;
            }

            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Compresses string to string.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <returns>Compressed string.</returns>
        public static string CompressString(this string source)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = source.Compress();

            if (buffer == null)
            {
                return null;
            }

            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Compresses string to string.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <param name="encoding">Text encoding.</param>
        /// <returns>Compressed string.</returns>
        public static async Task<string> CompressStringAsync(this string source, Encoding encoding)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = await source.CompressAsync(encoding).ConfigureAwait(false);

            if (buffer == null)
            {
                return null;
            }

            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Compresses string to string.
        /// </summary>
        /// <param name="source">Source string to be compressed.</param>
        /// <returns>Compressed string.</returns>
        public static async Task<string> CompressStringAsync(this string source)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = await source.CompressAsync().ConfigureAwait(false);

            if (buffer == null)
            {
                return null;
            }

            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// Decompress byte array to string.
        /// </summary>
        /// <param name="source">Source byte array to be decompressed.</param>
        /// <param name="encoding">Text encoding.</param>
        /// <returns>Decompressed string.</returns>
        public static string Decompress(this byte[] source, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (source == null)
            {
                return null;
            }

            using (var inputStream = new MemoryStream(source))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        gzipStream.CopyTo(outputStream);
                    }

                    return encoding.GetString(outputStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Decompress byte array to string.
        /// </summary>
        /// <param name="source">Source byte array to be decompressed.</param>
        /// <returns>Decompressed string.</returns>
        public static string Decompress(this byte[] source) => source.Decompress(Encoding.UTF8);

        /// <summary>
        /// Decompress byte array to string.
        /// </summary>
        /// <param name="source">Source byte array to be decompressed.</param>
        /// <param name="encoding">Text encoding.</param>
        /// <returns>Decompressed string.</returns>
        public static async Task<string> DecompressAsync(this byte[] source, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (source == null)
            {
                return null;
            }

            using (var inputStream = new MemoryStream(source))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        await gzipStream.CopyToAsync(outputStream).ConfigureAwait(false);
                    }

                    return encoding.GetString(outputStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Decompress byte array to string.
        /// </summary>
        /// <param name="source">Source byte array to be decompressed.</param>
        /// <returns>Decompressed string.</returns>
        public static Task<string> DecompressAsync(this byte[] source) => source.DecompressAsync(Encoding.UTF8);

        /// <summary>
        /// Decompresses string to string.
        /// </summary>
        /// <param name="source">Source string to be decompressed.</param>
        /// <param name="encoding">Text encoding</param>
        /// <returns>Decompressed string.</returns>
        public static string DecompressString(this string source, Encoding encoding)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = Convert.FromBase64String(source);

            if (buffer == null)
            {
                return null;
            }

            return buffer.Decompress(encoding);
        }

        /// <summary>
        /// Decompresses string to string.
        /// </summary>
        /// <param name="source">Source string to be decompressed.</param>
        /// <returns>Decompressed string.</returns>
        public static string DecompressString(this string source)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = Convert.FromBase64String(source);

            if (buffer == null)
            {
                return null;
            }

            return buffer.Decompress();
        }

        /// <summary>
        /// Decompresses string to string.
        /// </summary>
        /// <param name="source">Source string to be decompressed.</param>
        /// <param name="encoding">Text encoding</param>
        /// <returns>Decompressed string.</returns>
        public static async Task<string> DecompressStringAsync(this string source, Encoding encoding)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = Convert.FromBase64String(source);

            if (buffer == null)
            {
                return null;
            }

            return await buffer.DecompressAsync(encoding).ConfigureAwait(false);
        }

        /// <summary>
        /// Decompresses string to string.
        /// </summary>
        /// <param name="source">Source string to be decompressed.</param>
        /// <returns>Decompressed string.</returns>
        public static async Task<string> DecompressStringAsync(this string source)
        {
            if (source == null)
            {
                return null;
            }

            byte[] buffer = Convert.FromBase64String(source);

            if (buffer == null)
            {
                return null;
            }

            return await buffer.DecompressAsync().ConfigureAwait(false);
        }
    }
}
