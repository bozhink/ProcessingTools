// <copyright file="IBarcodeEncoder.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Processors.Imaging
{
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Barcode encoder.
    /// </summary>
    public interface IBarcodeEncoder
    {
        /// <summary>
        /// Encodes string content to barcode image with specified dimensions as byte array.
        /// </summary>
        /// <param name="type">Type of the barcode.</param>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="width">Width of the resultant image.</param>
        /// <param name="height">Height of the resultant image.</param>
        /// <returns>Barcode image as byte array.</returns>
        Task<byte[]> EncodeAsync(BarcodeType type, string content, int width, int height);

        /// <summary>
        /// Encodes string content to barcode image with specified dimensions as Base64 encoded string.
        /// </summary>
        /// <param name="type">Type of the barcode.</param>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="width">Width of the resultant image.</param>
        /// <param name="height">Height of the resultant image.</param>
        /// <returns>Barcode image as Base64 encoded string.</returns>
        Task<string> EncodeBase64Async(BarcodeType type, string content, int width, int height);
    }
}
