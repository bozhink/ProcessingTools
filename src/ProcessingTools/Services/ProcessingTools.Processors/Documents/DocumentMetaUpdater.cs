// <copyright file="DocumentMetaUpdater.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Documents
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Documents.Articles;
    using ProcessingTools.Models.Contracts.Documents.Journals;
    using ProcessingTools.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Processors.Contracts.Documents;

    /// <summary>
    /// Document meta-data updater.
    /// </summary>
    public class DocumentMetaUpdater : IDocumentMetaUpdater
    {
        /// <inheritdoc/>
        public Task<object> UpdateMetaAsync(IDocument document, IArticleMetaModel articleMeta, IJournalMetaModel journalMeta, IPublisherMetaModel publisherMeta)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (articleMeta == null)
            {
                throw new ArgumentNullException(nameof(articleMeta));
            }

            if (journalMeta == null)
            {
                throw new ArgumentNullException(nameof(journalMeta));
            }

            if (publisherMeta == null)
            {
                throw new ArgumentNullException(nameof(publisherMeta));
            }

            foreach (XmlNode articleNode in document.SelectNodes($".//{ElementNames.Article}"))
            {
                UpdateFront(document, articleMeta, journalMeta, publisherMeta, articleNode);
            }

            return Task.FromResult<object>(true);
        }

        private static void UpdateFront(IDocument document, IArticleMetaModel articleMeta, IJournalMetaModel journalMeta, IPublisherMetaModel publisherMeta, XmlNode articleNode)
        {
            XmlElement frontElement = articleNode[ElementNames.Front];
            if (frontElement == null)
            {
                frontElement = document.XmlDocument.CreateElement(ElementNames.Article);
                articleNode.PrependChild(frontElement);
            }

            UpdateJournalMeta(document, journalMeta, publisherMeta, frontElement);

            UpdateArticleMeta(document, articleMeta, frontElement);
        }

        private static void UpdateArticleMeta(IDocument document, IArticleMetaModel articleMeta, XmlElement frontElement)
        {
            XmlElement articleMetaElement = frontElement[ElementNames.ArticleMeta];
            if (articleMetaElement == null)
            {
                articleMetaElement = document.XmlDocument.CreateElement(ElementNames.ArticleMeta);
                frontElement.AppendChild(articleMetaElement);
            }

            UpdateArticleIdInternal(document, articleMeta, articleMetaElement);

            UpdateArticleIdDoi(document, articleMeta, articleMetaElement);

            UpdateTitleGroup(document, articleMeta, articleMetaElement);

            UpdateCollectionPublicationDate(document, articleMeta, articleMetaElement);

            UpdateElectronicPublicationDate(document, articleMeta, articleMetaElement);

            UpdateArchivalPublicationDate(document, articleMeta, articleMetaElement);

            UpdateSimpleElement(document, articleMetaElement, ElementNames.VolumeSeries, articleMeta.VolumeSeries);
            UpdateSimpleElement(document, articleMetaElement, ElementNames.Volume, articleMeta.Volume);
            UpdateSimpleElement(document, articleMetaElement, ElementNames.Issue, articleMeta.Issue);
            UpdateSimpleElement(document, articleMetaElement, ElementNames.IssuePart, articleMeta.IssuePart);
            UpdateSimpleElement(document, articleMetaElement, ElementNames.ELocationId, articleMeta.ELocationId);
            UpdateSimpleElement(document, articleMetaElement, ElementNames.FirstPage, articleMeta.FirstPage);
            UpdateSimpleElement(document, articleMetaElement, ElementNames.LastPage, articleMeta.LastPage);

            UpdateHistory(document, articleMeta, articleMetaElement);
        }

        private static void UpdateHistory(IDocument document, IArticleMetaModel articleMeta, XmlElement articleMetaElement)
        {
            XmlElement historyElement = articleMetaElement[ElementNames.History];
            if (articleMeta.AcceptedOn.HasValue || articleMeta.ReceivedOn.HasValue)
            {
                if (historyElement == null)
                {
                    historyElement = document.XmlDocument.CreateElement(ElementNames.History);
                    articleMetaElement.AppendChild(historyElement);
                }

                UpdateDateReceived(document, articleMeta, historyElement);

                UpdateDateAccepted(document, articleMeta, historyElement);
            }
            else
            {
                if (historyElement != null)
                {
                    articleMetaElement.RemoveChild(historyElement);
                }
            }
        }

        private static void UpdateDateAccepted(IDocument document, IArticleMetaModel articleMeta, XmlElement historyElement)
        {
            XmlElement dateAcceptedElement = historyElement.SelectSingleNode(XPathStrings.DateAccepted) as XmlElement;
            if (!articleMeta.AcceptedOn.HasValue)
            {
                if (dateAcceptedElement != null)
                {
                    historyElement.RemoveChild(dateAcceptedElement);
                }
            }
            else
            {
                if (dateAcceptedElement == null)
                {
                    dateAcceptedElement = document.XmlDocument.CreateElement(ElementNames.Date);

                    XmlAttribute dateTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.DateType);
                    dateTypeAttribute.InnerText = AttributeValues.Accepted;

                    dateAcceptedElement.Attributes.Append(dateTypeAttribute);

                    historyElement.AppendChild(dateAcceptedElement);
                }

                UpdateDayElement(document, dateAcceptedElement, articleMeta.AcceptedOn.Value.Day);
                UpdateMonthElement(document, dateAcceptedElement, articleMeta.AcceptedOn.Value.Month);
                UpdateYearElement(document, dateAcceptedElement, articleMeta.AcceptedOn.Value.Year);
            }
        }

        private static void UpdateDateReceived(IDocument document, IArticleMetaModel articleMeta, XmlElement historyElement)
        {
            XmlElement dateReceivedElement = historyElement.SelectSingleNode(XPathStrings.DateReceived) as XmlElement;
            if (!articleMeta.ReceivedOn.HasValue)
            {
                if (dateReceivedElement != null)
                {
                    historyElement.RemoveChild(dateReceivedElement);
                }
            }
            else
            {
                if (dateReceivedElement == null)
                {
                    dateReceivedElement = document.XmlDocument.CreateElement(ElementNames.Date);

                    XmlAttribute dateTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.DateType);
                    dateTypeAttribute.InnerText = AttributeValues.Received;

                    dateReceivedElement.Attributes.Append(dateTypeAttribute);

                    historyElement.AppendChild(dateReceivedElement);
                }

                UpdateDayElement(document, dateReceivedElement, articleMeta.ReceivedOn.Value.Day);
                UpdateMonthElement(document, dateReceivedElement, articleMeta.ReceivedOn.Value.Month);
                UpdateYearElement(document, dateReceivedElement, articleMeta.ReceivedOn.Value.Year);
            }
        }

        private static void UpdateSimpleElement(IDocument document, XmlElement parent, string name, string value)
        {
            XmlElement element = parent[name];
            if (element == null)
            {
                element = document.XmlDocument.CreateElement(name);
                parent.AppendChild(element);
            }

            element.InnerXml = value;
        }

        private static void UpdateArchivalPublicationDate(IDocument document, IArticleMetaModel articleMeta, XmlElement articleMetaElement)
        {
            XmlElement archivalPublicationDateElement = articleMetaElement.SelectSingleNode(XPathStrings.ArchivalPublicationDate) as XmlElement;
            if (!articleMeta.ArchivedOn.HasValue)
            {
                if (archivalPublicationDateElement != null)
                {
                    articleMetaElement.RemoveChild(archivalPublicationDateElement);
                }
            }
            else
            {
                if (archivalPublicationDateElement == null)
                {
                    archivalPublicationDateElement = document.XmlDocument.CreateElement(ElementNames.PubDate);

                    XmlAttribute pubTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.PubType);
                    pubTypeAttribute.InnerText = AttributeValues.PubTypeArchival;

                    archivalPublicationDateElement.Attributes.Append(pubTypeAttribute);

                    articleMetaElement.AppendChild(archivalPublicationDateElement);
                }

                UpdateDayElement(document, archivalPublicationDateElement, articleMeta.ArchivedOn.Value.Day);
                UpdateMonthElement(document, archivalPublicationDateElement, articleMeta.ArchivedOn.Value.Month);
                UpdateYearElement(document, archivalPublicationDateElement, articleMeta.ArchivedOn.Value.Year);
            }
        }

        private static void UpdateElectronicPublicationDate(IDocument document, IArticleMetaModel articleMeta, XmlElement articleMetaElement)
        {
            XmlElement electronicPublicationDateElement = articleMetaElement.SelectSingleNode(XPathStrings.ElectronicPublicationDate) as XmlElement;
            if (!articleMeta.PublishedOn.HasValue)
            {
                if (electronicPublicationDateElement != null)
                {
                    articleMetaElement.RemoveChild(electronicPublicationDateElement);
                }
            }
            else
            {
                if (electronicPublicationDateElement == null)
                {
                    electronicPublicationDateElement = document.XmlDocument.CreateElement(ElementNames.PubDate);

                    XmlAttribute pubTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.PubType);
                    pubTypeAttribute.InnerText = AttributeValues.PubTypeElectronic;

                    electronicPublicationDateElement.Attributes.Append(pubTypeAttribute);

                    articleMetaElement.AppendChild(electronicPublicationDateElement);
                }

                UpdateDayElement(document, electronicPublicationDateElement, articleMeta.PublishedOn.Value.Day);
                UpdateMonthElement(document, electronicPublicationDateElement, articleMeta.PublishedOn.Value.Month);
                UpdateYearElement(document, electronicPublicationDateElement, articleMeta.PublishedOn.Value.Year);
            }
        }

        private static void UpdateCollectionPublicationDate(IDocument document, IArticleMetaModel articleMeta, XmlElement articleMetaElement)
        {
            XmlElement collectionPublicationDateElement = articleMetaElement.SelectSingleNode(XPathStrings.CollectionPublicationDate) as XmlElement;
            if (!articleMeta.PublishedOn.HasValue)
            {
                if (collectionPublicationDateElement != null)
                {
                    articleMetaElement.RemoveChild(collectionPublicationDateElement);
                }
            }
            else
            {
                if (collectionPublicationDateElement == null)
                {
                    collectionPublicationDateElement = document.XmlDocument.CreateElement(ElementNames.PubDate);

                    XmlAttribute pubTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.PubType);
                    pubTypeAttribute.InnerText = AttributeValues.PubTypeCollection;

                    collectionPublicationDateElement.Attributes.Append(pubTypeAttribute);

                    articleMetaElement.AppendChild(collectionPublicationDateElement);
                }

                UpdateYearElement(document, collectionPublicationDateElement, articleMeta.PublishedOn.Value.Year);
            }
        }

        private static void UpdateDayElement(IDocument document, XmlElement parent, int value)
        {
            XmlElement dayElement = parent[ElementNames.Day];
            if (dayElement == null)
            {
                dayElement = document.XmlDocument.CreateElement(ElementNames.Day);
                parent.AppendChild(dayElement);
            }

            dayElement.InnerText = value.ToString();
        }

        private static void UpdateMonthElement(IDocument document, XmlElement parent, int value)
        {
            XmlElement monthElement = parent[ElementNames.Month];
            if (monthElement == null)
            {
                monthElement = document.XmlDocument.CreateElement(ElementNames.Month);
                parent.AppendChild(monthElement);
            }

            monthElement.InnerText = value.ToString();
        }

        private static void UpdateYearElement(IDocument document, XmlElement parent, int value)
        {
            XmlElement yearElement = parent[ElementNames.Year];
            if (yearElement == null)
            {
                yearElement = document.XmlDocument.CreateElement(ElementNames.Year);
                parent.AppendChild(yearElement);
            }

            yearElement.InnerText = value.ToString();
        }

        private static void UpdateTitleGroup(IDocument document, IArticleMetaModel articleMeta, XmlElement articleMetaElement)
        {
            XmlElement titleGroupElement = articleMetaElement[ElementNames.TitleGroup];
            if (titleGroupElement == null)
            {
                titleGroupElement = document.XmlDocument.CreateElement(ElementNames.TitleGroup);
                articleMetaElement.AppendChild(titleGroupElement);
            }

            UpdateArticleTitle(document, articleMeta, titleGroupElement);

            UpdateArticleSubTitle(document, articleMeta, titleGroupElement);
        }

        private static void UpdateArticleSubTitle(IDocument document, IArticleMetaModel articleMeta, XmlElement titleGroupElement)
        {
            if (!string.IsNullOrWhiteSpace(articleMeta.SubTitle))
            {
                XmlElement articleSubTitleElement = titleGroupElement[ElementNames.ArticleSubTitle];
                if (articleSubTitleElement == null)
                {
                    articleSubTitleElement = document.XmlDocument.CreateElement(ElementNames.ArticleSubTitle);
                    titleGroupElement.AppendChild(articleSubTitleElement);

                    // Overwrite article sub-title only if the entire node is missing.
                    // In other case we cannot provide correct XML stricture of the sub-title.
                    articleSubTitleElement.InnerText = articleMeta.SubTitle;
                }
            }
        }

        private static void UpdateArticleTitle(IDocument document, IArticleMetaModel articleMeta, XmlElement titleGroupElement)
        {
            if (!string.IsNullOrWhiteSpace(articleMeta.Title))
            {
                XmlElement articleTitleElement = titleGroupElement[ElementNames.ArticleTitle];
                if (articleTitleElement == null)
                {
                    articleTitleElement = document.XmlDocument.CreateElement(ElementNames.ArticleTitle);
                    titleGroupElement.PrependChild(articleTitleElement);

                    // Overwrite article title only if the entire node is missing.
                    // In other case we cannot provide correct XML stricture of the title.
                    articleTitleElement.InnerText = articleMeta.Title;
                }
            }
        }

        private static void UpdateArticleIdDoi(IDocument document, IArticleMetaModel articleMeta, XmlElement articleMetaElement)
        {
            XmlElement articleIdElement = articleMetaElement.SelectSingleNode(XPathStrings.ArticleIdDoi) as XmlElement;
            if (string.IsNullOrWhiteSpace(articleMeta.Doi))
            {
                if (articleIdElement != null)
                {
                    articleMetaElement.RemoveChild(articleIdElement);
                }
            }
            else
            {
                if (articleIdElement == null)
                {
                    articleIdElement = document.XmlDocument.CreateElement(ElementNames.ArticleId);

                    XmlAttribute pubIdTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.PubIdType);
                    pubIdTypeAttribute.InnerText = AttributeValues.PubIdTypeDoi;

                    articleIdElement.Attributes.Append(pubIdTypeAttribute);

                    articleMetaElement.PrependChild(articleIdElement);
                }

                articleIdElement.InnerText = articleMeta.Doi;
            }
        }

        private static void UpdateArticleIdInternal(IDocument document, IArticleMetaModel articleMeta, XmlElement articleMetaElement)
        {
            XmlElement articleIdElement = articleMetaElement.SelectSingleNode(XPathStrings.ArticleIdInternal) as XmlElement;
            if (string.IsNullOrWhiteSpace(articleMeta.ArticleId))
            {
                if (articleIdElement != null)
                {
                    articleMetaElement.RemoveChild(articleIdElement);
                }
            }
            else
            {
                if (articleIdElement == null)
                {
                    articleIdElement = document.XmlDocument.CreateElement(ElementNames.ArticleId);

                    XmlAttribute pubIdTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.PubIdType);
                    pubIdTypeAttribute.InnerText = AttributeValues.PubIdTypePublisherId;

                    articleIdElement.Attributes.Append(pubIdTypeAttribute);

                    articleMetaElement.PrependChild(articleIdElement);
                }

                articleIdElement.InnerText = articleMeta.ArticleId;
            }
        }

        private static void UpdateJournalMeta(IDocument document, IJournalMetaModel journalMeta, IPublisherMetaModel publisherMeta, XmlElement frontElement)
        {
            XmlElement journalMetaElement = frontElement[ElementNames.JournalMeta];
            if (journalMetaElement == null)
            {
                journalMetaElement = document.XmlDocument.CreateElement(ElementNames.JournalMeta);
                frontElement.PrependChild(journalMetaElement);
            }

            UpdateJournalId(document, journalMeta, journalMetaElement);

            UpdateJournalTitleGroup(document, journalMeta, journalMetaElement);

            UpdatePrintIssn(document, journalMeta, journalMetaElement);

            UpdateElectronicIssn(document, journalMeta, journalMetaElement);

            UpdatePublisher(document, publisherMeta, journalMetaElement);
        }

        private static void UpdateJournalId(IDocument document, IJournalMetaModel journalMeta, XmlElement journalMetaElement)
        {
            XmlElement journalIdElement = journalMetaElement[ElementNames.JournalId];
            if (journalIdElement == null || journalIdElement.Attributes[AttributeNames.JournalIdType]?.InnerText != AttributeValues.JournalIdTypePublisherId)
            {
                journalIdElement = document.XmlDocument.CreateElement(ElementNames.JournalId);

                XmlAttribute journalIdTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.JournalIdType);
                journalIdTypeAttribute.InnerText = AttributeValues.JournalIdTypePublisherId;

                journalIdElement.Attributes.Append(journalIdTypeAttribute);

                journalMetaElement.PrependChild(journalIdElement);
            }

            journalIdElement.InnerText = journalMeta.JournalId;
        }

        private static void UpdatePublisher(IDocument document, IPublisherMetaModel publisherMeta, XmlElement journalMetaElement)
        {
            XmlElement publisherElement = journalMetaElement[ElementNames.Publisher];
            if (publisherElement == null)
            {
                publisherElement = document.XmlDocument.CreateElement(ElementNames.Publisher);
                publisherElement.PrependChild(publisherElement);
            }

            UpdatePublisherName(document, publisherMeta, publisherElement);

            UpdatePublisherLocation(document, publisherMeta, publisherElement);
        }

        private static void UpdatePublisherLocation(IDocument document, IPublisherMetaModel publisherMeta, XmlElement publisherElement)
        {
            XmlElement publisherLocationElement = publisherElement[ElementNames.PublisherLocation];
            if (string.IsNullOrWhiteSpace(publisherMeta.Address))
            {
                if (publisherLocationElement != null)
                {
                    publisherElement.RemoveChild(publisherLocationElement);
                }
            }
            else
            {
                if (publisherLocationElement == null)
                {
                    publisherLocationElement = document.XmlDocument.CreateElement(ElementNames.PublisherLocation);
                    publisherElement.AppendChild(publisherLocationElement);
                }

                publisherLocationElement.InnerText = publisherMeta.Address;
            }
        }

        private static void UpdatePublisherName(IDocument document, IPublisherMetaModel publisherMeta, XmlElement publisherElement)
        {
            XmlElement publisherNameElement = publisherElement[ElementNames.PublisherName];
            if (publisherNameElement == null)
            {
                publisherNameElement = document.XmlDocument.CreateElement(ElementNames.PublisherName);
                publisherElement.PrependChild(publisherNameElement);
            }

            publisherNameElement.InnerText = publisherMeta.Name;
        }

        private static void UpdateElectronicIssn(IDocument document, IJournalMetaModel journalMeta, XmlElement journalMetaElement)
        {
            XmlElement electronicIssnElement = journalMetaElement.SelectSingleNode(XPathStrings.ElectronicIssn) as XmlElement;
            if (string.IsNullOrWhiteSpace(journalMeta.ElectronicIssn))
            {
                if (electronicIssnElement != null)
                {
                    journalMetaElement.RemoveChild(electronicIssnElement);
                }
            }
            else
            {
                if (electronicIssnElement == null)
                {
                    electronicIssnElement = document.XmlDocument.CreateElement(ElementNames.Issn);

                    XmlAttribute pubTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.PubType);
                    pubTypeAttribute.InnerText = AttributeValues.PubTypeElectronic;

                    electronicIssnElement.Attributes.Append(pubTypeAttribute);

                    journalMetaElement.AppendChild(electronicIssnElement);
                }

                electronicIssnElement.InnerText = journalMeta.ElectronicIssn;
            }
        }

        private static void UpdatePrintIssn(IDocument document, IJournalMetaModel journalMeta, XmlElement journalMetaElement)
        {
            XmlElement printIssnElement = journalMetaElement.SelectSingleNode(XPathStrings.PrintIssn) as XmlElement;
            if (string.IsNullOrWhiteSpace(journalMeta.PrintIssn))
            {
                if (printIssnElement != null)
                {
                    journalMetaElement.RemoveChild(printIssnElement);
                }
            }
            else
            {
                if (printIssnElement == null)
                {
                    printIssnElement = document.XmlDocument.CreateElement(ElementNames.Issn);

                    XmlAttribute pubTypeAttribute = document.XmlDocument.CreateAttribute(AttributeNames.PubType);
                    pubTypeAttribute.InnerText = AttributeValues.PubTypePrint;

                    printIssnElement.Attributes.Append(pubTypeAttribute);

                    journalMetaElement.AppendChild(printIssnElement);
                }

                printIssnElement.InnerText = journalMeta.PrintIssn;
            }
        }

        private static void UpdateJournalTitleGroup(IDocument document, IJournalMetaModel journalMeta, XmlElement journalMetaElement)
        {
            XmlElement journalTitleGroupElement = journalMetaElement[ElementNames.JournalTitleGroup];
            if (journalTitleGroupElement == null)
            {
                journalTitleGroupElement = document.XmlDocument.CreateElement(ElementNames.JournalTitleGroup);
                journalMetaElement.AppendChild(journalTitleGroupElement);
            }

            UpdateJournalTitle(document, journalMeta, journalTitleGroupElement);

            UpdateAbbrevJournalTitle(document, journalMeta, journalTitleGroupElement);
        }

        private static void UpdateAbbrevJournalTitle(IDocument document, IJournalMetaModel journalMeta, XmlElement journalTitleGroupElement)
        {
            XmlElement abbrevJournalTitleElement = journalTitleGroupElement[ElementNames.AbbrevJournalTitle];
            if (string.IsNullOrWhiteSpace(journalMeta.AbbreviatedName))
            {
                if (abbrevJournalTitleElement != null)
                {
                    journalTitleGroupElement.RemoveChild(abbrevJournalTitleElement);
                }
            }
            else
            {
                if (abbrevJournalTitleElement == null)
                {
                    abbrevJournalTitleElement = document.XmlDocument.CreateElement(ElementNames.AbbrevJournalTitle);
                    journalTitleGroupElement.AppendChild(abbrevJournalTitleElement);
                }

                abbrevJournalTitleElement.InnerText = journalMeta.AbbreviatedName;
            }
        }

        private static void UpdateJournalTitle(IDocument document, IJournalMetaModel journalMeta, XmlElement journalTitleGroupElement)
        {
            XmlElement journalTitleElement = journalTitleGroupElement[ElementNames.JournalTitle];
            if (journalTitleElement == null)
            {
                journalTitleElement = document.XmlDocument.CreateElement(ElementNames.JournalTitle);
                journalTitleGroupElement.PrependChild(journalTitleElement);
            }

            journalTitleElement.InnerText = journalMeta.Name;
        }
    }
}
