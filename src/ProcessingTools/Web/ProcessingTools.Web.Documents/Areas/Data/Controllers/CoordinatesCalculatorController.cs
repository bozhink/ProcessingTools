namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using ProcessingTools.Constants.Web;
    using ProcessingTools.Geo.Contracts.Parsers;
    using ProcessingTools.Web.Documents.Areas.Data.Models.CoordinatesCalculator;
    using ProcessingTools.Web.Documents.Areas.Data.ViewModels.CoordinatesCalculator;

    [Authorize]
    public class CoordinatesCalculatorController : Controller
    {
        private const string CoordinatesRequestModelValidationBindings = nameof(CoordinatesRequestModel.Coordinates);

        private readonly ICoordinateParser coordinateParser;

        public CoordinatesCalculatorController(ICoordinateParser coordinateParser)
        {
            if (coordinateParser == null)
            {
                throw new ArgumentNullException(nameof(coordinateParser));
            }

            this.coordinateParser = coordinateParser;
        }

        // GET: Data/CoordinatesCalculator
        [HttpGet]
        public ActionResult Index()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        // GET: Data/CoordinatesCalculator/Help
        [HttpGet]
        public ActionResult Help()
        {
            this.Response.StatusCode = (int)HttpStatusCode.OK;
            return this.View();
        }

        [HttpPost, ActionName(ActionNames.DeafultCalculateActionName)]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate([Bind(Include = CoordinatesRequestModelValidationBindings)]CoordinatesRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var coordinateStrings = model.Coordinates.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .Where(c => c.Length > 1)
                    .Distinct()
                    .ToArray();

                var viewModel = new CoordinatesResponseViewModel();
                foreach (var coordinateString in coordinateStrings)
                {
                    try
                    {
                        var coordinate = this.coordinateParser.ParseCoordinateString(coordinateString);

                        viewModel.Coordinates.Add(new CoordinateViewModel
                        {
                            Coordinate = coordinateString,
                            Latitude = coordinate.Latitude,
                            Longitude = coordinate.Longitude
                        });
                    }
                    catch
                    {
                        viewModel.Coordinates.Add(new CoordinateViewModel
                        {
                            Coordinate = coordinateString,
                            Latitude = "Error",
                            Longitude = "Error"
                        });
                    }
                }

                this.Response.StatusCode = (int)HttpStatusCode.OK;
                return this.View(viewModel);
            }

            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.Redirect(nameof(this.Index));
        }
    }
}
