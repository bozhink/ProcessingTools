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
            this.coordinateParser = coordinateParser ?? throw new ArgumentNullException(nameof(coordinateParser));
        }

        // GET: Data/CoordinatesCalculator
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        // GET: Data/CoordinatesCalculator/Help
        [HttpGet]
        public ActionResult Help()
        {
            return this.View();
        }

        [HttpPost, ActionName(ActionNames.DeafultCalculateActionName)]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate([Bind(Include = CoordinatesRequestModelValidationBindings)]CoordinatesRequestModel model)
        {
            if (this.ModelState.IsValid)
            {
                var coordinateStrings = model.Coordinates.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
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

                return this.View(viewModel);
            }

            this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.Redirect(nameof(this.Index));
        }
    }
}
