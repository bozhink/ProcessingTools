namespace ProcessingTools.Imaging.Processors
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using ImageProcessor;
    using ImageProcessor.Imaging;
    using ImageProcessor.Imaging.Formats;
    using ProcessingTools.Constants;
    using ProcessingTools.Imaging.Contracts.Processors;

    public class ImageProcessorService : IImageProcessorService
    {
        public Task<byte[]> Resize(byte[] originalImage, int width)
        {
            if (originalImage == null)
            {
                throw new ArgumentNullException(nameof(originalImage));
            }

            return Task.Run(() =>
            {
                using (var originalImageStream = new MemoryStream(originalImage))
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

        public Stream Resize(Stream originalImage, int width)
        {
            if (originalImage == null)
            {
                throw new ArgumentNullException(nameof(originalImage));
            }

            var resultImage = new MemoryStream();
            this.Resize(originalImage, width, resultImage);
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