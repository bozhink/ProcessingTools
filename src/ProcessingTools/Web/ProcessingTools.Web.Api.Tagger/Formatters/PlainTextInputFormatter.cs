// <copyright file="PlainTextInputFormatter.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Tagger.Formatters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Plain text input formatter.
    /// </summary>
    public class PlainTextInputFormatter : TextInputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextInputFormatter"/> class.
        /// </summary>
        public PlainTextInputFormatter()
        {
            this.SupportedMediaTypes.Clear();
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/text"));
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <inheritdoc/>
        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            return this.ReadRequestBodyInternalAsync(context, encoding);
        }

        /// <inheritdoc/>
        protected override bool CanReadType(Type type)
        {
            return true;
        }

        private async Task<InputFormatterResult> ReadRequestBodyInternalAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;

            using (var reader = new StreamReader(request.Body, encoding))
            {
                string content = await reader.ReadToEndAsync().ConfigureAwait(false);

                return await InputFormatterResult.SuccessAsync(content).ConfigureAwait(false);
            }
        }
    }
}
