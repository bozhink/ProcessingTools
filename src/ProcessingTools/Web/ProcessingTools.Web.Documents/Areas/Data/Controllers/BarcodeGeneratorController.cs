namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Processors.Imaging;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Web.Documents.Areas.Data.Models.BarcodeGenerator;
    using ProcessingTools.Web.Documents.Areas.Data.ViewModels.BarcodeGenerator;
    using ProcessingTools.Web.Documents.Extensions;

    [Authorize]
    public class BarcodeGeneratorController : Controller
    {
        private const string IndexRequestModelValidationIncludeBindings = nameof(IndexRequestModel.Width) + "," + nameof(IndexRequestModel.Height) + "," + nameof(IndexRequestModel.Type) + "," + nameof(IndexRequestModel.Content);

        private readonly IBarcodeEncoder encoder;

        public BarcodeGeneratorController(IBarcodeEncoder encoder)
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
            var viewModel = new IndexViewModel(0)
            {
                Width = ImagingConstants.DefaultBarcodeWidth,
                Height = ImagingConstants.DefaultBarcodeHeight
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Index([Bind(Include = IndexRequestModelValidationIncludeBindings)]IndexRequestModel model)
        {
            var viewModel = new IndexViewModel(model.Type)
            {
                Content = model.Content,
                Width = model.Width,
                Height = model.Height
            };

            try
            {
                if (this.ModelState.IsValid)
                {
                    viewModel.Image = await this.encoder.EncodeBase64Async((BarcodeType)model.Type, model.Content, model.Width, model.Height).ConfigureAwait(false);
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid data");
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(nameof(model.Content), e.Message);
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
