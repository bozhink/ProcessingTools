// <copyright file="EchoController.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Tagger.Controllers
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Echo controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        /// <summary>
        /// Respond on GET verb.
        /// </summary>
        /// <returns>Null as string.</returns>
        [HttpGet]
        public Task<string> GetAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "GET");
            return Task.FromResult<string>(null);
        }

        /// <summary>
        /// Respond on POST verb.
        /// </summary>
        /// <returns>Request content as string response.</returns>
        [HttpPost]
        public async Task<string> PostAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "POST");

            string content;

            using (var tr = new StreamReader(this.Request.Body))
            {
                content = await tr.ReadToEndAsync().ConfigureAwait(false);
            }

            return content;
        }

        /// <summary>
        /// Respond on PATCH verb.
        /// </summary>
        /// <returns>Request content as string response.</returns>
        [HttpPatch]
        public async Task<string> PatchAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "PATCH");

            string content;

            using (var tr = new StreamReader(this.Request.Body))
            {
                content = await tr.ReadToEndAsync().ConfigureAwait(false);
            }

            return content;
        }

        /// <summary>
        /// Respond on HEAD verb.
        /// </summary>
        /// <returns>Request content as string response.</returns>
        [HttpHead]
        public async Task<string> HeadAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "HEAD");

            string content;

            using (var tr = new StreamReader(this.Request.Body))
            {
                content = await tr.ReadToEndAsync().ConfigureAwait(false);
            }

            return content;
        }

        /// <summary>
        /// Respond on DELETE verb.
        /// </summary>
        /// <returns>Null as string.</returns>
        [HttpDelete]
        public Task<string> DeleteAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "DELETE");
            return Task.FromResult<string>(null);
        }
    }
}
