// <copyright file="TextBoxFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Text-box of type boxed-text.
    /// </summary>
    public class TextBoxFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//box[contains(string(title),'{this.FloatTypeNameInLabel}')]|//boxed-text[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.BoxedText;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Box";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Box|Boxes)";

        /// <inheritdoc/>
        public string InternalReferenceType => "boxed-text";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeTextBox;

        /// <inheritdoc/>
        public string Description => "Box";

        /// <inheritdoc/>
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
