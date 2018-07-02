// <copyright file="ITestFeaturesProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Special
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Test Features Provider
    /// </summary>
    public interface ITestFeaturesProvider
    {
        /// <summary>
        /// Extract System Checklist Authority
        /// </summary>
        /// <param name="document">Document context</param>
        void ExtractSystemChecklistAuthority(IDocument document);

        /// <summary>
        /// Move Authority Taxon Name Part To Taxon Authority Tag In TaxPub TpNomenclaure
        /// </summary>
        /// <param name="document">Document context</param>
        void MoveAuthorityTaxonNamePartToTaxonAuthorityTagInTaxPubTpNomenclaure(IDocument document);

        /// <summary>
        /// Re-numerate Foot-Notes
        /// </summary>
        /// <param name="document">Document context</param>
        void RenumerateFootNotes(IDocument document);

        /// <summary>
        /// Wrap Empty Superscripts In Footnote Xref Tag
        /// </summary>
        /// <param name="document">Document context</param>
        void WrapEmptySuperscriptsInFootnoteXrefTag(IDocument document);
    }
}
