// <copyright file="TaggerController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Commands.Models;
using ProcessingTools.Contracts.Commands.Tagger;
using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Documents;
using ProcessingTools.Contracts.Services.Layout;

namespace ProcessingTools.Web.Documents.Areas.Documents.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Web.Documents.Constants;

    /// <summary>
    /// /Documents/Tagger.
    /// </summary>
    [Authorize]
    [Area(AreaNames.Documents)]
    public class TaggerController : Controller
    {
        private readonly ILogger logger;
        private readonly IDocumentProcessingService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggerController"/> class.
        /// </summary>
        /// <param name="service">Service.</param>
        /// <param name="logger">Logger.</param>
        public TaggerController(IDocumentProcessingService service, ILogger<TaggerController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// /Documents/Tagger.
        /// </summary>
        /// <returns><see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// /Documents/Tagger/ParseReferences.
        /// </summary>
        /// <param name="documentId">Document object ID.</param>
        /// <param name="articleId">Article object ID.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> ParseReferences(string documentId, string articleId)
        {
            try
            {
                var result = await this.service.ParseReferencesAsync(documentId, articleId).ConfigureAwait(false);
                this.logger.LogInformation("{0}", result);
                return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Error", ex.ToString());
                this.logger.LogError(ex, "TaggerController.ParseReferences");
            }

            this.ViewData[ContextKeys.ReturnUrl] = this.Url.Action(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            return this.View();
        }

        /// <summary>
        /// /Documents/Tagger/TagReferences.
        /// </summary>
        /// <param name="documentId">Document object ID.</param>
        /// <param name="articleId">Article object ID.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> TagReferences(string documentId, string articleId)
        {
            try
            {
                var result = await this.service.TagReferencesAsync(documentId, articleId).ConfigureAwait(false);
                this.logger.LogInformation("{0}", result);
                return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Error", ex.ToString());
                this.logger.LogError(ex, "TaggerController.TagReferences");
            }

            this.ViewData[ContextKeys.ReturnUrl] = this.Url.Action(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            return this.View();
        }

        /// <summary>
        /// /Documents/Tagger/UpdateArticleDocumentsMeta.
        /// </summary>
        /// <param name="articleId">Article object ID.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> UpdateArticleDocumentsMeta(string articleId)
        {
            try
            {
                var result = await this.service.UpdateArticleDocumentsMetaAsync(articleId).ConfigureAwait(false);
                this.logger.LogInformation("{0}", result);
                return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Error", ex.ToString());
                this.logger.LogError(ex, "TaggerController.UpdateArticleDocumentsMeta");
            }

            this.ViewData[ContextKeys.ReturnUrl] = this.Url.Action(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            return this.View();
        }

        /// <summary>
        /// /Documents/Tagger/UpdateDocumentMeta.
        /// </summary>
        /// <param name="documentId">Document object ID.</param>
        /// <param name="articleId">Article object ID.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> UpdateDocumentMeta(string documentId, string articleId)
        {
            try
            {
                var result = await this.service.UpdateDocumentMetaAsync(documentId, articleId).ConfigureAwait(false);
                this.logger.LogInformation("{0}", result);
                return this.RedirectToAction(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("Error", ex.ToString());
                this.logger.LogError(ex, "TaggerController.UpdateDocumentMetaAsync");
            }

            this.ViewData[ContextKeys.ReturnUrl] = this.Url.Action(ArticlesController.DocumentsActionName, ArticlesController.ControllerName, new { id = articleId });
            return this.View();
        }

        /// <summary>
        /// Tagger core.
        /// </summary>
        public class TaggerCore
        {
            private readonly Func<Type, ITaggerCommand> commandFactory;
            private readonly IFactory<ICommandSettings> commandSettingsFactory;
            private readonly IDocumentFactory documentFactory;
            private readonly IDocumentPostReadNormalizer documentReadNormalizer;
            private readonly IDocumentPreWriteNormalizer documentWriteNormalizer;
            private IDictionary<Type, ICommandInfo> commandsInformation;

            /// <summary>
            /// Initializes a new instance of the <see cref="TaggerCore"/> class.
            /// </summary>
            /// <param name="commandInfoProvider">Instance of <see cref="ICommandInfoProvider"/>.</param>
            /// <param name="documentFactory">Instance of <see cref="IDocumentFactory"/>.</param>
            /// <param name="documentReadNormalizer">Instance of <see cref="IDocumentPostReadNormalizer"/>.</param>
            /// <param name="documentWriteNormalizer">Instance of <see cref="IDocumentPreWriteNormalizer"/>.</param>
            /// <param name="commandFactory">Command factory.</param>
            /// <param name="commandSettingsFactory">Command settings factory.</param>
            public TaggerCore(
                ICommandInfoProvider commandInfoProvider,
                IDocumentFactory documentFactory,
                IDocumentPostReadNormalizer documentReadNormalizer,
                IDocumentPreWriteNormalizer documentWriteNormalizer,
                Func<Type, ITaggerCommand> commandFactory,
                IFactory<ICommandSettings> commandSettingsFactory)
            {
                if (commandInfoProvider == null)
                {
                    throw new ArgumentNullException(nameof(commandInfoProvider));
                }

                this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
                this.documentReadNormalizer = documentReadNormalizer ?? throw new ArgumentNullException(nameof(documentReadNormalizer));
                this.documentWriteNormalizer = documentWriteNormalizer ?? throw new ArgumentNullException(nameof(documentWriteNormalizer));
                this.commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
                this.commandSettingsFactory = commandSettingsFactory ?? throw new ArgumentNullException(nameof(commandSettingsFactory));

                this.GetCommands(commandInfoProvider);
            }

            /// <summary>
            /// Executes command specified by ID.
            /// </summary>
            /// <param name="commandId">ID of the command to be executed.</param>
            /// <param name="xmldocument"><see cref="XmlDocument"/> to be processed.</param>
            /// <returns>Processed <see cref="XmlDocument"/>.</returns>
            /// <example>
            ///  ```
            ///  var xmldocument = new XmlDocument(); // Read document
            /// await this.RunCommand(model, xmldocument)
            ///    .ContinueWith(_ =>
            ///    {
            ///        _.Wait();
            ///       // Write document
            ///        return;
            ///    })
            ///    .ConfigureAwait(false);
            /// ```
            /// .
            /// </example>
            public async Task<XmlDocument> RunCommand(string commandId, XmlDocument xmldocument)
            {
                var document = this.documentFactory.Create(xmldocument.OuterXml);
                if (document.XmlDocument.DocumentElement.Name == ElementNames.Article)
                {
                    document.SchemaType = SchemaType.Nlm;
                }
                else
                {
                    document.SchemaType = SchemaType.System;
                }

                await this.documentReadNormalizer.NormalizeAsync(document).ConfigureAwait(false);

                var commandType = this.commandsInformation
                    .First(p => p.Value.Name == commandId)
                    .Key;

                var command = this.commandFactory.Invoke(commandType);
                var settings = this.commandSettingsFactory.Create();

                var result = await command.RunAsync(document, settings)
                    .ContinueWith(_ =>
                    {
                        _.Wait();
                        return this.documentWriteNormalizer.NormalizeAsync(document);
                    })
                    .ContinueWith(_ =>
                    {
                        _.Wait();
                        return document.XmlDocument;
                    })
                    .ConfigureAwait(false);

                return result;
            }

            private void GetCommands(ICommandInfoProvider commandInfoProvider)
            {
                commandInfoProvider.ProcessInformation();

                var commandsInformations = commandInfoProvider.CommandsInformation
                    .Where(p => p.Key.GetInterfaces()
                    .Contains(typeof(ISimpleTaggerCommand)));

                this.commandsInformation = new Dictionary<Type, ICommandInfo>();
                foreach (var commandInformation in commandsInformations)
                {
                    this.commandsInformation.Add(commandInformation.Key, commandInformation.Value);
                }
            }

            private IList<CommandSelectListItem> GetCommandsAsSelectList()
            {
                return this.commandsInformation.Values.OrderBy(i => i.Description)
                    .Select(c => new CommandSelectListItem
                    {
                        CommandValue = c.Name,
                        CommandText = c.Description,
                    })
                    .ToList();
            }

            /// <summary>
            /// Command select list item.
            /// </summary>
            public class CommandSelectListItem
            {
                /// <summary>
                /// Gets or sets the command description as the text field.
                /// </summary>
                public string CommandText { get; set; }

                /// <summary>
                /// Gets or sets the command ID as the value field.
                /// </summary>
                public string CommandValue { get; set; }
            }
        }
    }
}
