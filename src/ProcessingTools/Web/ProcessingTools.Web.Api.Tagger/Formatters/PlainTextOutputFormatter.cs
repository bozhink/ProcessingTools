// <copyright file="PlainTextOutputFormatter.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Tagger.Formatters
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Plain text output formatter.
    /// </summary>
    public class PlainTextOutputFormatter : TextOutputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextOutputFormatter"/> class.
        /// </summary>
        public PlainTextOutputFormatter()
        {
            this.SupportedMediaTypes.Clear();
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/text"));
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <inheritdoc/>
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (selectedEncoding is null)
            {
                throw new ArgumentNullException(nameof(selectedEncoding));
            }

            return this.WriteResponseBodyInternalAsync(context);
        }

        /// <inheritdoc/>
        protected override bool CanWriteType(Type type)
        {
            return true;
        }

        private async Task WriteResponseBodyInternalAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            await response.WriteAsync(context.Object?.ToString() ?? string.Empty).ConfigureAwait(false);
        }
    }
}
