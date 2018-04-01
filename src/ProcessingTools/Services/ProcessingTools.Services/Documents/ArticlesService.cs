// <copyright file="ArticlesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using AutoMapper;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Models.Contracts.Documents.Articles;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.History;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;
    using ProcessingTools.Services.Models.Documents.Articles;

    /// <summary>
    /// Articles service.
    /// </summary>
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesDataService articlesDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesService"/> class.
        /// </summary>
        /// <param name="articlesDataService">Articles data service.</param>
        public ArticlesService(IArticlesDataService articlesDataService)
        {
            this.articlesDataService = articlesDataService ?? throw new ArgumentNullException(nameof(articlesDataService));
        }

        /// <inheritdoc/>
        public Task<object> DeleteAsync(object id) => this.articlesDataService.DeleteAsync(id);

        /// <inheritdoc/>
        public Task<IArticleJournalModel[]> GetArticleJournalsAsync() => this.articlesDataService.GetArticleJournalsAsync();

        /// <inheritdoc/>
        public Task<IArticleDetailsModel> GetDetailsByIdAsync(object id) => this.articlesDataService.GetDetailsByIdAsync(id);

        /// <inheritdoc/>
        public Task<object> CreateAsync(IArticleInsertModel model) => this.articlesDataService.InsertAsync(model);

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.articlesDataService.SelectCountAsync();

        /// <inheritdoc/>
        public Task<IArticleDetailsModel[]> SelectDetailsAsync(int skip, int take) => this.articlesDataService.SelectDetailsAsync(skip, take);

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IArticleUpdateModel model) => this.articlesDataService.UpdateAsync(model);

        /// <inheritdoc/>
        public Task<object> CreateFromFileAsync(IArticleFileModel model, Stream stream, string journalId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("File stream can not be read.");
            }

            XmlDocument xmlDocument = GetXmlDocument(stream);

            xmlDocument.DocumentElement.SelectNodes("");



            throw new NotImplementedException();
        }

        private static XmlDocument GetXmlDocument(Stream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                Async = true,
                CloseInput = true,
                ConformanceLevel = ConformanceLevel.Document,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = false,
                IgnoreProcessingInstructions = false,
                IgnoreWhitespace = false,
                ValidationType = ValidationType.None
            };

            XmlReader reader = XmlReader.Create(stream, settings);

            XmlDocument xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xmlDocument.Load(reader);
            return xmlDocument;
        }
    }
}
