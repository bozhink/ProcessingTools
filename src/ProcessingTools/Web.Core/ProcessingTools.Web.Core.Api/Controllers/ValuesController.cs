// <copyright file="ValuesController.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Core.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Values controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values

        /// <summary>
        /// Gets values.
        /// </summary>
        /// <returns>List of values.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new[] { "value1", "value2" });
        }

        // GET api/values/5

        /// <summary>
        /// Gets value by ID.
        /// </summary>
        /// <param name="id">ID of the value.</param>
        /// <returns>Selected value.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return this.Ok("value");
        }

        // POST api/values

        /// <summary>
        /// Adds value in the list of values.
        /// </summary>
        /// <param name="value">Value to be added.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return this.NoContent();
        }

        // PUT api/values/5

        /// <summary>
        /// Updates value at selected ID.
        /// </summary>
        /// <param name="id">ID of the value to be updated.</param>
        /// <param name="value">New value to be set.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return this.NoContent();
        }

        // DELETE api/values/5

        /// <summary>
        /// Deletes value at selected ID.
        /// </summary>
        /// <param name="id">ID of the value.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return this.NoContent();
        }
    }
}
