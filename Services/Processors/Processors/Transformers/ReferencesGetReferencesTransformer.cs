namespace ProcessingTools.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class ReferencesGetReferencesTransformer : XslTransformer<IReferencesGetReferencesXslTransformProvider>, IReferencesGetReferencesTransformer
    {
        public ReferencesGetReferencesTransformer(IReferencesGetReferencesXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}
