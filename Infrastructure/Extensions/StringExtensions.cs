namespace ProcessingTools.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using ProcessingTools.Common;

    public static class StringExtensions
    {
        public static Stream ToStream(this string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Stream stream = null;

            try
            {
                byte[] bytesContent = Defaults.DefaultEncoding.GetBytes(content);
                stream = new MemoryStream(bytesContent);
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException($"Input document string should be {Defaults.DefaultEncoding.EncodingName} encoded.", e);
            }

            return stream;
        }
    }
}
