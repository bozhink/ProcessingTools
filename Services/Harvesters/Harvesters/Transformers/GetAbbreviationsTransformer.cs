namespace ProcessingTools.Harvesters.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class GetAbbreviationsTransformer : XQueryTransformer<IGetAbbreviationsXQueryTransformProvider>, IGetAbbreviationsTransformer
    {
        public GetAbbreviationsTransformer(IGetAbbreviationsXQueryTransformProvider xqueryTransformProvider)
            : base(xqueryTransformProvider)
        {
        }
    }
}
