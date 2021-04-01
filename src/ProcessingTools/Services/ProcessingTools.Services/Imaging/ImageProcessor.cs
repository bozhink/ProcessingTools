// <copyright file="ImageProcessor.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Imaging
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Imaging;
    using SkiaSharp;

    /// <summary>
    /// Image processor.
    /// </summary>
    public class ImageProcessor : IImageProcessor
    {
        /// <inheritdoc/>
        public Task<byte[]> ResizeAsync(byte[] sourceImage, int width)
        {
            if (sourceImage is null)
            {
                throw new ArgumentNullException(nameof(sourceImage));
            }

            return Task.Run(() =>
            {
                using (var originalImageStream = new MemoryStream(sourceImage))
                {
                    using (var resultImage = new MemoryStream())
                    {
                        this.Resize(originalImageStream, width, resultImage);
                        var result = resultImage.GetBuffer();

                        resultImage.Close();
                        originalImageStream.Close();

                        return result;
                    }
                }
            });
        }

        /// <inheritdoc/>
        public Stream Resize(Stream sourceImage, int width)
        {
            if (sourceImage is null)
            {
                throw new ArgumentNullException(nameof(sourceImage));
            }

            var resultImage = new MemoryStream();
            this.Resize(sourceImage, width, resultImage);
            return resultImage;
        }

        private void Resize(Stream originalImage, int size, Stream resultImage)
        {
            using (var inputStream = new SKManagedStream(originalImage))
            {
                using (var original = SKBitmap.Decode(inputStream))
                {
                    int width, height;
                    if (original.Width > original.Height)
                    {
                        width = size;
                        height = original.Height * size / original.Width;
                    }
                    else
                    {
                        width = original.Width * size / original.Height;
                        height = size;
                    }

                    using (var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High))
                    {
                        if (resized is null)
                        {
                            return;
                        }

                        using (var image = SKImage.FromBitmap(resized))
                        {
                            image.Encode(SKEncodedImageFormat.Jpeg, ImagingConstants.JpegImageQuality)
                                .SaveTo(resultImage);
                        }
                    }
                }
            }
        }
    }
}
