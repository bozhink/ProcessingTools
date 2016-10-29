namespace ProcessingTools.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class FormatTaxonTreatmentsTransformer : XslTransformer<IFormatTaxonTreatmentsXslTransformProvider>, IFormatTaxonTreatmentsTransformer
    {
        public FormatTaxonTreatmentsTransformer(IFormatTaxonTreatmentsXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}
