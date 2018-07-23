// <copyright file="CoordinatesCalculatorController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Tools.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Processors.Contracts.Geo.Coordinates;
    using ProcessingTools.Web.Documents.Constants;
    using ProcessingTools.Web.Models.Tools.Coordinates;

    /// <summary>
    /// CoordinatesCalculator
    /// </summary>
    [Authorize]
    [Area(AreaNames.Tools)]
    public class CoordinatesCalculatorController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "CoordinatesCalculator";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        private readonly ICoordinateParser coordinateParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinatesCalculatorController"/> class.
        /// </summary>
        /// <param name="coordinateParser">Instance of <see cref="ICoordinateParser"/>.</param>
        public CoordinatesCalculatorController(ICoordinateParser coordinateParser)
        {
            this.coordinateParser = coordinateParser ?? throw new ArgumentNullException(nameof(coordinateParser));
        }

        /// <summary>
        /// GET CoordinatesCalculator
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [HttpGet]
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// POST CoordinatesCalculator
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns><see cref="IActionResult"/></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName(IndexActionName)]
        public IActionResult Index([Bind(nameof(CoordinatesRequestModel.Coordinates))]CoordinatesRequestModel model)
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

            return this.View();
        }

        /// <summary>
        /// Help
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(ActionNames.Help)]
        public IActionResult Help()
        {
            return this.View();
        }
    }
}
