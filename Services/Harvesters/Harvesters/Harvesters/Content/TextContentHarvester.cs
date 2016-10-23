namespace ProcessingTools.Harvesters.Content
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Content;
    using Contracts.Transformers;

    public class TextContentHarvester : ITextContentHarvester
    {
        private readonly IGetTextContentTransformer transformer;

        public TextContentHarvester(IGetTextContentTransformer transformer)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            this.transformer = transformer;
        }

        public async Task<string> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformer.Transform(context);
            content = Regex.Replace(content, @"(?<=\n)\s+", string.Empty);

            return content;
        }
    }
}
