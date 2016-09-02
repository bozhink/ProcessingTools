namespace ProcessingTools.BaseLibrary.Abbreviations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    public class AbbreviationsTagger : IAbbreviationsTagger
    {
        private readonly IAbbreviationsContextTagger contextTagger;

        public AbbreviationsTagger(IAbbreviationsContextTagger contextTagger)
        {
            if (contextTagger == null)
            {
                throw new ArgumentNullException(nameof(contextTagger));
            }

            this.contextTagger = contextTagger;
        }

        public async Task<object> Tag(XmlNode context)
        {
            // Do not change this sequence
            await this.TagAbbreviationsInSpecificNodeByXPath(context, "//graphic | //media | //disp-formula-group");
            await this.TagAbbreviationsInSpecificNodeByXPath(context, "//chem-struct-wrap | //fig | //supplementary-material | //table-wrap");
            await this.TagAbbreviationsInSpecificNodeByXPath(context, "//fig-group | //table-wrap-group");
            await this.TagAbbreviationsInSpecificNodeByXPath(context, "//boxed-text");
            await this.TagAbbreviationsInSpecificNodeByXPath(context, "//alt-title | //article-title | //attrib | //award-id | //comment | //conf-theme | //def-head | //element-citation | //funding-source | //license-p | //meta-value | //mixed-citation | //p | //preformat | //product | //subtitle | //supplement | //td | //term | //term-head | //th | //title | //trans-subtitle | //trans-title | //verse-line");

            return true;
        }

        private async Task TagAbbreviationsInSpecificNodeByXPath(XmlNode context, string selectSpecificNodeXPath)
        {
            var tasks = context.SelectNodes(selectSpecificNodeXPath)
                .Cast<XmlNode>()
                .Select(n => this.contextTagger.Tag(n))
                .ToArray();

            await Task.WhenAll(tasks);
        }
    }
}
