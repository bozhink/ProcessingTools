namespace ProcessingTools.Imaging.Processors
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using global::ImageProcessor;
    using global::ImageProcessor.Imaging;
    using global::ImageProcessor.Imaging.Formats;
    using ProcessingTools.Constants;
    using ProcessingTools.Processors.Contracts.Imaging;

    public class ImageProcessor : IImageProcessor
    {
        public Task<byte[]> ResizeAsync(byte[] sourceImage, int width)
        {
            if (sourceImage == null)
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

        public Stream Resize(Stream sourceImage, int width)
        {
            if (sourceImage == null)
            {
                throw new ArgumentNullException(nameof(sourceImage));
            }

            var resultImage = new MemoryStream();
            this.Resize(sourceImage, width, resultImage);
            return resultImage;
        }

        private void Resize(Stream originalImage, int width, Stream resultImage)
        {
            using (var imageFactory = new ImageFactory())
            {
                var createdImage = imageFactory.Load(originalImage);

                if (createdImage.Image.Width > width)
                {
                    var size = new Size(width, 0);
                    var resizeLayer = new ResizeLayer(size, ResizeMode.Max);
                    createdImage = createdImage.Resize(resizeLayer);
                }

                createdImage.Format(new JpegFormat
                {
                    Quality = ImagingConstants.JpegImageQuality
                })
                .Save(resultImage);
            }
        }
    }
}