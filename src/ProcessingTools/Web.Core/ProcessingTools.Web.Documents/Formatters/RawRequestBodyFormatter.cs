// <copyright file="RawRequestBodyFormatter.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Formatters
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using ProcessingTools.Constants;

    /// <summary>
    /// Raw request body formatter.
    /// See https://weblog.west-wind.com/posts/2017/Sep/14/Accepting-Raw-Request-Body-Content-in-ASPNET-Core-API-Controllers
    /// </summary>
    public class RawRequestBodyFormatter : InputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RawRequestBodyFormatter"/> class.
        /// </summary>
        public RawRequestBodyFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentTypes.PlainText));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentTypes.OctetStream));
        }

        /// <summary>
        /// Allow text/plain, application/octet-stream and no content type to
        /// be processed
        /// </summary>
        /// <param name="context">Input formatter context.</param>
        /// <returns>Is formatter applicable.</returns>
        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var contentType = context.HttpContext.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) || contentType == ContentTypes.PlainText || contentType == ContentTypes.OctetStream)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Handle text/plain or no content type for string results
        /// Handle application/octet-stream for byte[] results
        /// </summary>
        /// <param name="context">Input formatter context.</param>
        /// <returns>Input formatter result.</returns>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;

            if (string.IsNullOrEmpty(contentType) || contentType == ContentTypes.PlainText)
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = await reader.ReadToEndAsync().ConfigureAwait(false);
                    return await InputFormatterResult.SuccessAsync(content).ConfigureAwait(false);
                }
            }

            if (contentType == ContentTypes.OctetStream)
            {
                using (var ms = new MemoryStream(2048))
                {
                    await request.Body.CopyToAsync(ms).ConfigureAwait(false);
                    var content = ms.ToArray();
                    return await InputFormatterResult.SuccessAsync(content).ConfigureAwait(false);
                }
            }

            return await InputFormatterResult.FailureAsync().ConfigureAwait(false);
        }
    }
}
