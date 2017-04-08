namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Image.Processors.Contracts.Processors;
    using ProcessingTools.Web.Documents.Areas.Data.Models.QRCodeGenerator;
    using ProcessingTools.Web.Documents.Areas.Data.ViewModels.QRCodeGenerator;

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

        // GET: Data/QRCodeGenerator
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                PixelPerModule = 5
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
                    viewModel.Image = await this.encoder.EncodeSvg(model.Content, viewModel.PixelPerModule);
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
    }
}
