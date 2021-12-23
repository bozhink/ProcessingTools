// <copyright file="VcardInputFormatter.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Formatters
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Web.Documents.Models;

    /// <summary>
    /// VCARD input formatter.
    /// </summary>
    public class VcardInputFormatter : TextInputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VcardInputFormatter"/> class.
        /// </summary>
        public VcardInputFormatter()
        {
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
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
            if (type == typeof(Contact))
            {
                return base.CanReadType(type);
            }

            return false;
        }

        private async Task<string> ReadLineAsync(string expectedText, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync().ConfigureAwait(false);

            if (!line.StartsWith(expectedText, false, CultureInfo.InvariantCulture))
            {
                var errorMessage = $"Looked for '{expectedText}' and got '{line}'";
                context.ModelState.TryAddModelError(context.ModelName, errorMessage);
                throw new InvalidFormatException(errorMessage);
            }

            return line;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Not applicable")]
        private async Task<InputFormatterResult> ReadRequestBodyInternalAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;

            using (var reader = new StreamReader(request.Body, encoding))
            {
                try
                {
                    await this.ReadLineAsync("BEGIN:VCARD", reader, context).ConfigureAwait(false);
                    await this.ReadLineAsync("VERSION:2.1", reader, context).ConfigureAwait(false);

                    var nameLine = await this.ReadLineAsync("N:", reader, context).ConfigureAwait(false);
                    var split = nameLine.Split(";".ToCharArray());
                    var contact = new Contact { LastName = split[0].Substring(2), FirstName = split[1] };

                    await this.ReadLineAsync("FN:", reader, context).ConfigureAwait(false);

                    var idLine = await this.ReadLineAsync("UID:", reader, context).ConfigureAwait(false);
                    contact.Id = idLine.Substring(4);

                    await this.ReadLineAsync("END:VCARD", reader, context).ConfigureAwait(false);

                    return await InputFormatterResult.SuccessAsync(contact).ConfigureAwait(false);
                }
                catch
                {
                    return await InputFormatterResult.FailureAsync().ConfigureAwait(false);
                }
            }
        }
    }
}
