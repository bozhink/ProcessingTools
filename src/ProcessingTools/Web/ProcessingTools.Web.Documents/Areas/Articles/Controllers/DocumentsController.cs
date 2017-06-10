namespace ProcessingTools.Web.Documents.Areas.Articles.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Constants.Web;
    using ProcessingTools.Web.Documents.Abstractions;
    using ProcessingTools.Web.Documents.Areas.Articles.ViewModels.Documents;

    public class DocumentsController : MvcControllerWithExceptionHandling
    {
        protected override string InstanceName => InstanceNames.DocumentsControllerInstanceName;

        // TODO: To be removed
        private int FakeArticleId => 0;

        // GET: /Articles/Documents
        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToAction(
                actionName: nameof(FilesController.Index),
                controllerName: ControllerNames.FilesControllerName);
        }

        // GET: /Articles/Documents/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var viewModel = new DocumentIdViewModel(this.FakeArticleId, id);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: /Articles/Documents/Preview/5
        [HttpGet]
        public ActionResult Preview(Guid? id)
        {
            if (id == null)
            {
                throw new InvalidIdException();
            }

            var viewModel = new DocumentIdViewModel(this.FakeArticleId, id);

            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View(viewModel);
        }

        // GET: /Articles/Documents/Help
        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }
    }
}
