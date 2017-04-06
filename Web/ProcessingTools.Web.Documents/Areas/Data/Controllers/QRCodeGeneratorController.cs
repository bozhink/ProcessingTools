namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ProcessingTools.Image.Processors.Contracts.Processors;

    [Authorize]
    public class QRCodeGeneratorController : Controller
    {
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
            return this.View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string content)
        {
            try
            {
                var image = await this.encoder.EncodeSvg(content, 5);

                return this.View(model: image);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(nameof(content), e);
                return this.View();
            }
        }
    }
}
