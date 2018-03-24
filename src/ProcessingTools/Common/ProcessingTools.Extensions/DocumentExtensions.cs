// <copyright file="DocumentExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Document extensions.
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Generates file name from document id.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> to be harvested.</param>
        /// <returns>File name.</returns>
        public static string GenerateFileNameFromDocumentId(this IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string articleId = document.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText ?? string.Empty;

            string referencesFileName = articleId.ToLowerInvariant()
                .RegexReplace(@"\A.*/", string.Empty)
                .RegexReplace(@"\W+", "-")
                .Trim(new[] { ' ', '-' });

            if (string.IsNullOrWhiteSpace(referencesFileName))
            {
                referencesFileName = Guid.NewGuid().ToString();
            }

            return referencesFileName;
        }
    }
}
