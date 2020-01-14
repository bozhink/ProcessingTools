// <copyright file="ITestFeaturesProvider.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Special
{
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Test features provider.
    /// </summary>
    public interface ITestFeaturesProvider
    {
        /// <summary>
        /// Extract system checklist authority.
        /// </summary>
        /// <param name="document">Document context.</param>
        void ExtractSystemChecklistAuthority(IDocument document);

        /// <summary>
        /// Move authority taxon name part to taxon authority tag in TaxPub tp:nomenclature.
        /// </summary>
        /// <param name="document">Document context.</param>
        void MoveAuthorityTaxonNamePartToTaxonAuthorityTagInTaxPubTpNomenclature(IDocument document);

        /// <summary>
        /// Re-numerate foot-notes.
        /// </summary>
        /// <param name="document">Document context.</param>
        void RenumerateFootNotes(IDocument document);

        /// <summary>
        /// Wrap empty superscripts in footnote xref tag.
        /// </summary>
        /// <param name="document">Document context.</param>
        void WrapEmptySuperscriptsInFootnoteXrefTag(IDocument document);
    }
}
