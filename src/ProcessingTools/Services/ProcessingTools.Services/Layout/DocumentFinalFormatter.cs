﻿// <copyright file="DocumentFinalFormatter.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Layout;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Document final formatter.
    /// </summary>
    public class DocumentFinalFormatter : IDocumentFinalFormatter
    {
        /// <inheritdoc/>
        public Task<object> FormatAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SelectNodes("//kwd")
                .AsParallel()
                .ForAll(node =>
                {
                    node.InnerXml = node.InnerXml
                        .RegexReplace(@"\s+", " ")
                        .RegexReplace(@"(?<=<italic>)\s+|\s+(?=</italic>)|\s+(?=</tp:taxon-name>)|(?<=<tp:taxon-name(\s+[^>]*)?>)\s+", string.Empty)
                        .Trim();
                });

            context.Xml = context.Xml.RegexReplace(@"\s*(</tp:taxon-name>)\s+(?=<comment>[,;:\.])", "$1");

            return Task.FromResult<object>(true);
        }
    }
}
