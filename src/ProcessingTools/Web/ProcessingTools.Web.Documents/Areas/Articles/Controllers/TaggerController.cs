namespace ProcessingTools.Web.Documents.Areas.Articles.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Xml;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Constants.Web;
    using ProcessingTools.Contracts;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Exceptions;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;
    using ProcessingTools.Tagger.Commands.Contracts;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Contracts.Models;
    using ProcessingTools.Tagger.Commands.Contracts.Providers;
    using ProcessingTools.Web.Documents.Abstractions;
    using ProcessingTools.Web.Documents.Areas.Articles.Models.Tagger;
    using ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Tagger;

    public class TaggerController : MvcControllerWithExceptionHandling
    {
        private const string DocumentValidationBinding = nameof(FileModel.DocumentId) + "," + nameof(FileModel.FileName) + "," + nameof(FileModel.Comment) + "," + nameof(FileModel.CommandId);

        private readonly Func<Type, ITaggerCommand> commandFactory;
        private readonly IFactory<ICommandSettings> commandSettingsFactory;
        private readonly IDocumentFactory documentFactory;
        private readonly IDocumentPostReadNormalizer documentReadNormalizer;
        private readonly IDocumentPreWriteNormalizer documentWriteNormalizer;
        private readonly IDocumentsDataService service;
        private readonly IDictionary<Type, ICommandInfo> commandsInformation;

        public TaggerController(
            ICommandInfoProvider commandInfoProvider,
            IDocumentsDataService service,
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

            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.documentReadNormalizer = documentReadNormalizer ?? throw new ArgumentNullException(nameof(documentReadNormalizer));
            this.documentWriteNormalizer = documentWriteNormalizer ?? throw new ArgumentNullException(nameof(documentWriteNormalizer));
            this.commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            this.commandSettingsFactory = commandSettingsFactory ?? throw new ArgumentNullException(nameof(commandSettingsFactory));

            commandInfoProvider.ProcessInformation();

            var commandsInformation = commandInfoProvider.CommandsInformation
                .Where(p => p.Key.GetInterfaces()
                .Contains(typeof(ISimpleTaggerCommand)));

            this.commandsInformation = new Dictionary<Type, ICommandInfo>();
            foreach (var commandInformation in commandsInformation)
            {
                this.commandsInformation.Add(commandInformation.Key, commandInformation.Value);
            }
        }

        protected override string InstanceName => InstanceNames.FilesControllerInstanceName;

        // TODO: To be removed
        private int FakeArticleId => 0;

        private string UserId => this.User.Identity.GetUserId();

        // GET: /Articles/Tagger
        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction(
                actionName: nameof(FilesController.Index),
                controllerName: ControllerNames.FilesControllerName);
        }

        // GET: /Articles/Tagger/Edit/5
        [HttpGet, ActionName(ActionNames.DeafultEditActionName)]
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var userId = this.UserId;
            var articleId = this.FakeArticleId;

            var viewModel = await this.GetDetails(userId, articleId, id);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // POST: /Articles/Tagger/Edit/5
        [HttpPost, ActionName(ActionNames.DeafultEditActionName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = DocumentValidationBinding)] FileModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userId = this.UserId;
                var articleId = this.FakeArticleId;

                var xmldocument = await this.ReadDocument(model, userId, articleId);
                await this.RunCommand(model, xmldocument)
                    .ContinueWith(_ =>
                    {
                        _.Wait();
                        return this.WriteDocument(model, userId, articleId, _.Result).Result;
                    });

                this.Response.StatusCode = (int)HttpStatusCode.Redirect;
                return this.RedirectToAction(nameof(this.Index));
            }

            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.RedirectToAction(nameof(this.Edit), new { id = model.DocumentId });
        }

        // GET: /Articles/Help
        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }

        private SelectList GetCommandsAsSelectList()
        {
            return new SelectList(
                items: this.commandsInformation.Values.OrderBy(i => i.Description),
                dataValueField: nameof(ICommandInfo.Name),
                dataTextField: nameof(ICommandInfo.Description));
        }

        private async Task<FileViewModel> GetDetails(object userId, object articleId, object id)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var document = await this.service.Get(userId, articleId, id);
            if (document == null)
            {
                throw new EntityNotFoundException();
            }

            var viewModel = new FileViewModel
            {
                ArticleId = articleId.ToString(),
                DocumentId = document.Id.ToString(),
                FileName = document.FileName,
                FileExtension = document.FileExtension,
                Comment = document.Comment,
                ContentType = document.ContentType,
                ContentLength = document.ContentLength,
                DateCreated = document.DateCreated,
                DateModified = document.DateModified,
                CommandId = this.GetCommandsAsSelectList()
            };

            return viewModel;
        }

        private async Task<XmlDocument> ReadDocument(FileModel model, string userId, int articleId)
        {
            XmlDocument document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var reader = await this.service.GetReader(userId, articleId, model.DocumentId);

            document.Load(reader);
            return document;
        }

        private async Task<XmlDocument> RunCommand(FileModel model, XmlDocument xmldocument)
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

            await this.documentReadNormalizer.Normalize(document);

            var commandType = this.commandsInformation
                .First(p => p.Value.Name == model.CommandId)
                .Key;

            var command = this.commandFactory.Invoke(commandType);
            var settings = this.commandSettingsFactory.Create();

            var result = await command.Run(document, settings)
                .ContinueWith(_ =>
                {
                    _.Wait();
                    return this.documentWriteNormalizer.Normalize(document);
                })
                .ContinueWith(_ =>
                {
                    _.Wait();
                    return document.XmlDocument;
                });

            return result;
        }

        private async Task<object> WriteDocument(FileModel model, string userId, int articleId, XmlDocument document)
        {
            using (var stream = document.OuterXml.ToStream())
            {
                string comment = this.commandsInformation
                    .Values
                    .FirstOrDefault(i => i.Name == model.CommandId)
                    .Description;

                var documentMetadata = new DocumentServiceModel
                {
                    Comment = comment,
                    ContentLength = stream.Length,
                    ContentType = ContentTypes.Xml,
                    FileExtension = FileConstants.XmlFileExtension,
                    FileName = model.FileName
                };

                var result = await this.service.Create(userId, articleId, documentMetadata, stream);

                stream.Close();

                return result;
            }
        }
    }
}
