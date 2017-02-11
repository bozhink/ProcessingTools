namespace ProcessingTools.Layout.Processors.Processors.Formatters
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Formatters;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    public class DocumentFinalFormatter : IDocumentFinalFormatter
    {
        public async Task<object> Format(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            document.SelectNodes("//kwd")
                .AsParallel()
                .ForAll(node =>
                {
                    node.InnerXml = node.InnerXml
                        .RegexReplace(@"\s+", " ")
                        .RegexReplace(@"(?<=<italic>)\s+|\s+(?=</italic>)|\s+(?=</tp:taxon-name>)|(?<=<tp:taxon-name(\s+[^>]*)?>)\s+", string.Empty)
                        .Trim();
                });

            document.Xml = document.Xml.RegexReplace(@"\s*(</tp:taxon-name>)\s+(?=<comment>[,;:\.])", "$1");

            return await Task.FromResult(true);
        }
    }
}
