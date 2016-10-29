namespace ProcessingTools.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class ZooBankRegistrationXmlTransformer : XslTransformer<IZooBankRegistrationXmlXslTransformProvider>, IZooBankRegistrationXmlTransformer
    {
        public ZooBankRegistrationXmlTransformer(IZooBankRegistrationXmlXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}
