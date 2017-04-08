namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Imaging.Constants;
    using ProcessingTools.Imaging.Contracts.Processors;
    using ProcessingTools.Web.Documents.Areas.Data.Models.QRCodeGenerator;
    using ProcessingTools.Web.Documents.Areas.Data.ViewModels.QRCodeGenerator;
    using ProcessingTools.Web.Documents.Extensions;

    [Authorize]
    public class QRCodeGeneratorController : Controller
    {
        private const string IndexRequestModelValidationIncludeBindings = nameof(IndexRequestModel.PixelPerModule) + "," + nameof(IndexRequestModel.Content);

        private readonly IQRCodeEncoder encoder;

        public QRCodeGeneratorController(IQRCodeEncoder encoder)
        {
            if (encoder == null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            this.encoder = encoder;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                PixelPerModule = DefaultConstants.PixelPerModule
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Index([Bind(Include = IndexRequestModelValidationIncludeBindings)]IndexRequestModel model)
        {
            var viewModel = new IndexViewModel
            {
                Content = model.Content,
                PixelPerModule = model.PixelPerModule
            };

            try
            {
                if (this.ModelState.IsValid)
                {
                    viewModel.Image = await this.encoder.EncodeBase64(model.Content, viewModel.PixelPerModule);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid data");
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(nameof(model.Content), e);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Help()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.IvalidActionErrorView(actionName)
                .ExecuteResult(this.ControllerContext);
        }
    }
}
