// <copyright file="SystemController.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// System controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        /// <summary>
        /// Gets the version of entry assembly.
        /// </summary>
        /// <returns>Version of the entry assembly as string.</returns>
        [HttpGet("version")]
        public string GetVersion()
        {
            return typeof(Startup).Assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// Gets the versions of all assemblies.
        /// </summary>
        /// <returns>Versions of all assemblies.</returns>
        [HttpGet("versions")]
        public IActionResult GetVersions()
        {
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            return this.Ok(assemblies.ToDictionary(a => a.FullName, a => a.GetName().Version.ToString()));
        }

        /// <summary>
        /// Gets the status of the system.
        /// </summary>
        /// <returns>Status of the system.</returns>
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return this.Ok(new
            {
                Status = 0,
            });
        }
    }
}