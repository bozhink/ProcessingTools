namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Nlm.Publishing.Constants;
    using Types;

    /// <summary>
    /// Text-box of type boxed-text.
    /// </summary>
    internal class TextBoxFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//box[contains(string(title),'{this.FloatTypeNameInLabel}')]|//boxed-text[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Textbox;

        public string FloatTypeNameInLabel => "Box";

        public string MatchCitationPattern => @"(?:Box|Boxes)";

        public string InternalReferenceType => "boxed-text";

        public string ResultantReferenceType => RefTypeAttributeValues.TextBox;

        public string Description => "Box";
    }
}
