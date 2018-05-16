// <copyright file="GoogleChartsController.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Test.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Web.Documents.Areas.Test.Models;
    using ProcessingTools.Web.Documents.Constants;

    /// <summary>
    /// /Test/GoogleCharts
    /// </summary>
    /// <remarks>
    /// See https://www.codeproject.com/Articles/1243682/Create-Cross-Platform-Charts-with-ASP-NET-Core-MVC
    /// </remarks>
    [Authorize]
    [Area(AreaNames.Test)]
    public class GoogleChartsController : Controller
    {
        /// <summary>
        /// Controller name.
        /// </summary>
        public const string ControllerName = "GoogleCharts";

        /// <summary>
        /// Index action name.
        /// </summary>
        public const string IndexActionName = nameof(Index);

        /// <summary>
        /// Use DataTable action name.
        /// </summary>
        public const string UseDataTableActionName = nameof(UseDataTable);

        /// <summary>
        /// Use DataArray action name.
        /// </summary>
        public const string UseDataArrayActionName = nameof(UseDataArray);

        /// <summary>
        /// Use JSON data action name.
        /// </summary>
        public const string UseJsonDataActionName = nameof(UseJsonData);

        /// <summary>
        /// Use data from server action name.
        /// </summary>
        public const string UseDataFromServerActionName = nameof(UseDataFromServer);

        /// <summary>
        /// JSON data action name.
        /// </summary>
        public const string JsonDataActionName = nameof(JsonData);

        /// <summary>
        /// Real time chart action name.
        /// </summary>
        public const string UseRealTimeDataActionName = nameof(UseRealTimeData);

        /// <summary>
        /// Get real time data action name.
        /// </summary>
        public const string GetRealTimeDataActionName = nameof(GetRealTimeData);

        private readonly Random random = new Random();

        /// <summary>
        /// /Test/GoogleCharts
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(IndexActionName)]
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// /Test/GoogleCharts/UseDataTable
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(UseDataTableActionName)]
        public IActionResult UseDataTable()
        {
            return this.View();
        }

        /// <summary>
        /// /Test/GoogleCharts/UseDataArray
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(UseDataArrayActionName)]
        public IActionResult UseDataArray()
        {
            return this.View();
        }

        /// <summary>
        /// /Test/GoogleCharts/UseJsonData
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(UseJsonDataActionName)]
        public IActionResult UseJsonData()
        {
            return this.View();
        }

        /// <summary>
        /// /Test/GoogleCharts/UseDataFromServer
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(UseDataFromServerActionName)]
        public IActionResult UseDataFromServer()
        {
            return this.View();
        }

        /// <summary>
        /// /Test/GoogleCharts/JsonData
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(JsonDataActionName)]
        public IActionResult JsonData()
        {
            var data = ModelHelper.MultiLineData();
            return this.Json(data);
        }

        /// <summary>
        /// /Test/GoogleCharts/RealTimeChart
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(UseRealTimeDataActionName)]
        public IActionResult UseRealTimeData()
        {
            return this.View();
        }

        /// <summary>
        /// /Test/GoogleCharts/GetRealTimeData
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [ActionName(GetRealTimeDataActionName)]
        public IActionResult GetRealTimeData()
        {
            RealTimeData data = new RealTimeData
            {
                TimeStamp = DateTime.Now,
                DataValue = this.random.Next(0, 11)
            };

            return this.Json(data);
        }
    }
}
