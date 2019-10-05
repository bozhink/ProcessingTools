// <copyright file="SystemController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// System controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        /// <summary>
        /// Gets the version of current assembly.
        /// </summary>
        /// <returns></returns>
        [HttpGet("version")]
        public string GetVersion()
        {
            return typeof(Startup).Assembly.GetName().Version.ToString();
        }
    }
}