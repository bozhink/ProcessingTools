namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Text-box of type boxed-text.
    /// </summary>
    internal class TextBoxFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//box[contains(string(title),'{this.FloatTypeNameInLabel}')]|//boxed-text[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.BoxedText;

        public string FloatTypeNameInLabel => "Box";

        public string MatchCitationPattern => @"(?:Box|Boxes)";

        public string InternalReferenceType => "boxed-text";

        public string ResultantReferenceType => AttributeValues.RefTypeTextBox;

        public string Description => "Box";
    }
}
