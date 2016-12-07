namespace ProcessingTools.Harvesters.Harvesters.Content
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Harvesters.Contracts.Factories;

    public class TextContentHarvester : ITextContentHarvester
    {
        private readonly ITextContentTransformersFactory transformersFactory;

        public TextContentHarvester(ITextContentTransformersFactory transformersFactory)
        {
            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.transformersFactory = transformersFactory;
        }

        public async Task<string> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformersFactory
                .GetTextContentTransformer()
                .Transform(context);
            content = Regex.Replace(content, @"(?<=\n)\s+", string.Empty);

            return content;
        }
    }
}
