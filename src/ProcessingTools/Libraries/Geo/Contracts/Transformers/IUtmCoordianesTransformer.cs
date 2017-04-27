namespace ProcessingTools.Geo.Contracts.Transformers
{
    public interface IUtmCoordianesTransformer
    {
        double[] TransformDecimal2Utm(double latitude, double longitude, string utmZone);

        double[] TransformUtm2Decimal(double utmEasting, double utmNorthing, string utmZone);
    }
}
