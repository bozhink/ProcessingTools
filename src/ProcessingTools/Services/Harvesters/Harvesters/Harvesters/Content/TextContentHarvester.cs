namespace ProcessingTools.Harvesters.Harvesters.Content
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Harvesters.Contracts.Factories;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Content;

    public class TextContentHarvester : ITextContentHarvester
    {
        private readonly ITextContentTransformersFactory transformersFactory;

        public TextContentHarvester(ITextContentTransformersFactory transformersFactory)
        {
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public async Task<string> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformersFactory
                .GetTextContentTransformer()
                .TransformAsync(context);

            content = Regex.Replace(content, @"(?<=\n)\s+", string.Empty);

            return content;
        }
    }
}
