namespace ProcessingTools.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class ReferencesTagTemplateTransformer : XslTransformer<IReferencesTagTemplateXslTransformProvider>, IReferencesTagTemplateTransformer
    {
        public ReferencesTagTemplateTransformer(IReferencesTagTemplateXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}
