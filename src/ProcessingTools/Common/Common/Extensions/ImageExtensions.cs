namespace ProcessingTools.Common.Extensions
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// See http://stackoverflow.com/questions/7350679/convert-a-bitmap-into-a-byte-array
    /// See http://stackoverflow.com/questions/10889764/how-to-convert-bitmap-to-a-base64-string
    /// </summary>
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }

        public static string ToBase64String(this Image image, ImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                var array = stream.ToArray();
                return Convert.ToBase64String(array);
            }
        }
    }
}
