namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Textbox of type boxed-text.
    /// </summary>
    public class TextBoxFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//box[contains(string(title),'{this.FloatTypeNameInLabel}')]|//boxed-text[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Textbox;

        public string FloatTypeNameInLabel => "Box";

        public string MatchCitationPattern => @"(?:Box|Boxes)";

        public string InternalReferenceType => "boxed-text";
    }
}