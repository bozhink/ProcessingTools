// <copyright file="DocumentExtensions.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Extensions
{
    using System;
    using System.Text.RegularExpressions;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;

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
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string articleId = document.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText ?? string.Empty;

            Regex matchSlash = new Regex(@"\A.*/", RegexOptions.Compiled);
            Regex matchNonCharacter = new Regex(@"\W+", RegexOptions.Compiled);

            string referencesFileName = matchNonCharacter.Replace(matchSlash.Replace(articleId.ToLowerInvariant(), string.Empty), "-")
                .Trim(new[] { ' ', '-' });

            if (string.IsNullOrWhiteSpace(referencesFileName))
            {
                referencesFileName = Guid.NewGuid().ToString();
            }

            return referencesFileName;
        }
    }
}
