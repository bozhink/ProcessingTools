namespace ProcessingTools.Layout.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class SystemInitialFormatTransformer : XslTransformer<ISystemInitialFormatXslTransformProvider>, ISystemInitialFormatTransformer
    {
        public SystemInitialFormatTransformer(ISystemInitialFormatXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}
