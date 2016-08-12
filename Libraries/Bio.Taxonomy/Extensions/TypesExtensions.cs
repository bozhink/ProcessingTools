namespace ProcessingTools.Bio.Taxonomy.Extensions
{
    using Types;

    public static class TypesExtensions
    {
        public static string MapTaxonRankTypeToTaxonRankString(this TaxonRankType rank) => rank.ToString().ToLower();
    }
}
