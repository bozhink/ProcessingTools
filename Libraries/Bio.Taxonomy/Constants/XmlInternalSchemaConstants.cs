namespace ProcessingTools.Bio.Taxonomy.Constants
{
    public static class XmlInternalSchemaConstants
    {
        public const string IdAttributeName = "id";

        public const string TaxonNameElementName = "tn";
        public const string TaxonNameTypeAttributeName = "type";
        public const string TaxonNamePartElementName = "tn-part";
        public const string TaxonNamePartRankAttributeName = "type";
        public const string TaxonNamePartFullNameAttributeName = "full-name";

        public const string TaxonNamePartOfTypeGenusXPath = "tn-part[@type='genus']";
        public const string TaxonNamePartOfTypeSpeciesXPath = "tn-part[@type='species']";
    }
}
