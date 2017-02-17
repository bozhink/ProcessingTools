namespace ProcessingTools.Web.Documents.Areas.Data.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Models.CoordinatesCalculator;
    using ProcessingTools.Geo.Contracts;
    using ProcessingTools.Geo.Contracts.Models;
    using ProcessingTools.Geo.Models;
    using ProcessingTools.Web.Common.Constants;
    using ViewModels.CoordinatesCalculator;

    public class CoordinatesCalculatorController : Controller
    {
        private const string CoordinatesRequestModelValidationBindings = nameof(CoordinatesRequestModel.Coordinates);

        private readonly ICoordinate2DParser coordinate2DParser;

        public CoordinatesCalculatorController(ICoordinate2DParser coordinate2DParser)
        {
            if (coordinate2DParser == null)
            {
                throw new ArgumentNullException(nameof(coordinate2DParser));
            }

            this.coordinate2DParser = coordinate2DParser;
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
                    var latitude = new CoordinatePart();
                    var longitude = new CoordinatePart();
                    try
                    {
                        this.coordinate2DParser.ParseCoordinateString(coordinateString, null, latitude, longitude);

                        viewModel.Coordinates.Add(new CoordinateViewModel
                        {
                            Coordinate = coordinateString,
                            Latitude = latitude.Value,
                            Longitude = longitude.Value
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
