// <copyright file="IQRCodeEncoder.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Imaging.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// QR-code encoder.
    /// </summary>
    public interface IQRCodeEncoder
    {
        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as byte array.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as byte array.</returns>
        Task<byte[]> EncodeAsync(string content, int pixelPerModule);

        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as Base64 encoded string.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as Base64 string.</returns>
        Task<string> EncodeBase64Async(string content, int pixelPerModule);
    }
}
