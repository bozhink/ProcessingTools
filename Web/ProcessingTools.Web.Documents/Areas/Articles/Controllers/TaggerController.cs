namespace ProcessingTools.Web.Documents.Areas.Articles.Controllers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Constants;
    using ProcessingTools.Documents.Services.Data.Contracts;
    using ProcessingTools.Documents.Services.Data.Models;
    using ProcessingTools.Extensions;
    using ProcessingTools.Web.Common.Constants;
    using ProcessingTools.Web.Common.ViewModels;
    using ProcessingTools.Web.Documents.Areas.Articles.Models.Tagger;
    using ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Tagger;
    using ProcessingTools.Web.Documents.Extensions;
    using ProcessingTools.Web.Documents.Abstractions;
    using Strings = Resources.Strings;
    using ProcessingTools.Tagger.Commands.Contracts.Providers;
    using ProcessingTools.Tagger.Commands.Contracts.Models;
    using System.Xml;

    public class TaggerController : MvcControllerWithExceptionHandling
    {
        private const string DocumentValidationBinding = nameof(FileModel.DocumentId) + "," + nameof(FileModel.FileName) + "," + nameof(FileModel.FileExtension) + "," + nameof(FileModel.ContentType) + "," + nameof(FileModel.Comment) + "," + nameof(FileModel.CommandId);

        private readonly ICommandInfoProvider commandInfoProvider;
        private readonly IDocumentsDataService service;

        public TaggerController(
            ICommandInfoProvider commandInfoProvider,
            IDocumentsDataService service)
        {
            if (commandInfoProvider == null)
            {
                throw new ArgumentNullException(nameof(commandInfoProvider));
            }

            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.commandInfoProvider = commandInfoProvider;
            this.service = service;

            this.commandInfoProvider.ProcessInformation();
        }



        private string UserId => User.Identity.GetUserId();

        // TODO: To be removed
        private int FakeArticleId => 0;

        protected override string InstanceName => InstanceNames.FilesControllerInstanceName;

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
            if (ModelState.IsValid)
            {
                var userId = this.UserId;
                var articleId = this.FakeArticleId;

                XmlDocument document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                var reader = await this.service.GetReader(userId, articleId, model.DocumentId);

                document.Load(reader);

                using (var stream = document.OuterXml.ToStream())
                {
                    var documentMetadata = new DocumentServiceModel
                    {
                        Comment = model.CommandId,
                        ContentLength = stream.Length,
                        ContentType = "application/xml",
                        FileExtension = "xml",
                        FileName = model.FileName
                    };

                    var result = await this.service.Create(userId, articleId, documentMetadata, stream);
                }

                this.Response.StatusCode = (int)HttpStatusCode.Redirect;
                return this.RedirectToAction(nameof(this.Index));
            }

            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.View(model);
        }

        // GET: /Articles/Help
        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
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
                CommandId = GetCommandsAsSelectList()
            };

            return viewModel;
        }

        private SelectList GetCommandsAsSelectList()
        {
            return new SelectList(
                items: this.commandInfoProvider.CommandsInformation.Values,
                dataValueField: nameof(ICommandInfo.Name),
                dataTextField: nameof(ICommandInfo.Description));
        }
    }
}
