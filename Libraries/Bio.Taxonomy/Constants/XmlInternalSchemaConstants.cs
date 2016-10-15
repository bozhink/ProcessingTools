namespace ProcessingTools.Bio.Taxonomy.Constants
{
    public static class XmlInternalSchemaConstants
    {
        public const string IdAttributeName = "id";
        public const string PositionAttributeName = "position";
        public const string TypeAttributeName = "type";
        public const string FullNameAttributeName = "full-name";

        public const string TaxonNameElementName = "tn";
        public const string TaxonNamePartElementName = "tn-part";

        public const string TaxonNameIdPrefix = "TN";
        public const string TaxonNamePartIdPrefix = "TNP";

        public const string TaxonNamePartOfTypeGenusXPath = "tn-part[@type='genus']";
        public const string TaxonNamePartOfTypeSpeciesXPath = "tn-part[@type='species']";

        public const string SelectLowerTaxonNamesXPath = ".//tn[@type='lower']";
        public const string SelectTaxonNamePartsOfLowerTaxonNamesXPath = SelectLowerTaxonNamesXPath + "/" + TaxonNamePartElementName;

        public const string LowerTaxonTypeValue = "lower";
        public const string HigherTaxonTypeValue = "higher";
    }
}
