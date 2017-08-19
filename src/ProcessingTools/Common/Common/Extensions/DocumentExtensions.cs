namespace ProcessingTools.Common.Extensions
{
    using System;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;

    public static class DocumentExtensions
    {
        public static string GenerateFileNameFromDocumentId(this IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            string articleId = document.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText ?? string.Empty;

            string referencesFileName = articleId.ToLower()
                .RegexReplace(@"\A.*/", string.Empty)
                .RegexReplace(@"\W+", "-")
                .Trim(new char[] { ' ', '-' });

            if (string.IsNullOrWhiteSpace(referencesFileName))
            {
                referencesFileName = Guid.NewGuid().ToString();
            }

            return referencesFileName;
        }
    }
}
