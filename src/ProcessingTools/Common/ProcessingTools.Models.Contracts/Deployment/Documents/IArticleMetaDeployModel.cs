// <copyright file="IArticleMetaDeployModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Deployment.Documents
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Article meta deploy model.
    /// </summary>
    public interface IArticleMetaDeployModel : IDeployModel
    {
        /// <summary>
        /// Gets the value of the title as text.
        /// </summary>
        string TitleText { get; }

        /// <summary>
        /// Gets the value of the title as XML.
        /// </summary>
        string TitleXml { get; }

        /// <summary>
        /// Gets the value of the subtitle as text.
        /// </summary>
        string SubTitleText { get; }

        /// <summary>
        /// Gets the value of the subtitle as XML.
        /// </summary>
        string SubTitleXML { get; }

        /// <summary>
        /// Gets the Digital Object Identifier (DOI) of the article.
        /// </summary>
        string Doi { get; }

        /// <summary>
        /// Gets the journal ID.
        /// </summary>
        string JournalId { get; }

        /// <summary>
        /// Gets the journal name.
        /// </summary>
        string JournalName { get; }

        /// <summary>
        /// Gets the journal abbreviated name.
        /// </summary>
        string JournalAbbreviatedName { get; }

        /// <summary>
        /// Gets the journal print ISSN.
        /// </summary>
        string JournalPrintIssn { get; }

        /// <summary>
        /// Gets the journal electronic ISSN.
        /// </summary>
        string JournalElectronicIssn { get; }

        /// <summary>
        /// Gets the publisher name.
        /// </summary>
        string PublisherName { get; }

        /// <summary>
        /// Gets the publisher address.
        /// </summary>
        string PublisherAddress { get; }

        /// <summary>
        /// Gets the published date.
        /// </summary>
        DateTime? PublishedOn { get; }

        /// <summary>
        /// Gets the accepted date.
        /// </summary>
        DateTime? AcceptedOn { get; }

        /// <summary>
        /// Gets the received date.
        /// </summary>
        DateTime? ReceivedOn { get; }

        /// <summary>
        /// Gets the volume series.
        /// </summary>
        string VolumeSeries { get; }

        /// <summary>
        /// Gets the volume.
        /// </summary>
        string Volume { get; }

        /// <summary>
        /// Gets the issue.
        /// </summary>
        string Issue { get; }

        /// <summary>
        /// Gets the issue part.
        /// </summary>
        string IssuePart { get; }

        /// <summary>
        /// Gets the e-location ID.
        /// </summary>
        string ELocationId { get; }

        /// <summary>
        /// Gets the first page.
        /// </summary>
        string FirstPage { get; }

        /// <summary>
        /// Gets the last page.
        /// </summary>
        string LastPage { get; }

        /// <summary>
        /// Gets the number of pages.
        /// </summary>
        int NumberOfPages { get; }

        /// <summary>
        /// Gets the list of abstracts.
        /// </summary>
        IEnumerable<IArticleAbstractDeployModel> Abstracts { get; }
    }
}
