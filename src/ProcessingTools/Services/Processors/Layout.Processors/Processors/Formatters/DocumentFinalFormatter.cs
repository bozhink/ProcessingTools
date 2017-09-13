namespace ProcessingTools.Layout.Processors.Processors.Formatters
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Formatters;

    public class DocumentFinalFormatter : IDocumentFinalFormatter
    {
        public async Task<object> FormatAsync(IDocument context)
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

            return await Task.FromResult(true).ConfigureAwait(false);
        }
    }
}
