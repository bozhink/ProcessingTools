// <copyright file="VcardOutputFormatter.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

// See https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-3.0.
namespace ProcessingTools.Web.Documents.Formatters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using ProcessingTools.Web.Documents.Models;

    /// <summary>
    /// VCARD output formatter.
    /// </summary>
    public class VcardOutputFormatter : TextOutputFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VcardOutputFormatter"/> class.
        /// </summary>
        public VcardOutputFormatter()
        {
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
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
            if (typeof(Contact).IsAssignableFrom(type) || typeof(IEnumerable<Contact>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        private static void FormatVcard(StringBuilder buffer, Contact contact)
        {
            buffer.AppendLine("BEGIN:VCARD");
            buffer.AppendLine("VERSION:2.1");
            buffer.AppendLine($"N:{contact.LastName};{contact.FirstName}");
            buffer.AppendLine($"FN:{contact.FirstName} {contact.LastName}");
            buffer.AppendLine($"UID:{contact.Id}");
            buffer.AppendLine("END:VCARD");
        }

        private async Task WriteResponseBodyInternalAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<Contact>)
            {
                foreach (Contact contact in context.Object as IEnumerable<Contact>)
                {
                    FormatVcard(buffer, contact);
                }
            }
            else
            {
                var contact = context.Object as Contact;
                FormatVcard(buffer, contact);
            }

            await response.WriteAsync(buffer.ToString()).ConfigureAwait(false);
        }
    }
}
