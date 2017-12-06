// <copyright file="IImageProcessor.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Processors.Imaging
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Image processor.
    /// </summary>
    public interface IImageProcessor
    {
        /// <summary>
        /// Resizes image to specified width.
        /// </summary>
        /// <param name="sourceImage">Image to be resized as byte array.</param>
        /// <param name="width">Required width of the resultant image.</param>
        /// <returns>Resized image as byte array.</returns>
        Task<byte[]> ResizeAsync(byte[] sourceImage, int width);

        /// <summary>
        /// Resizes image to specified width.
        /// </summary>
        /// <param name="sourceImage">Image to be resized as stream.</param>
        /// <param name="width">Required width of the resultant image.</param>
        /// <returns>Resized image as stream.</returns>
        Stream Resize(Stream sourceImage, int width);
    }
}
