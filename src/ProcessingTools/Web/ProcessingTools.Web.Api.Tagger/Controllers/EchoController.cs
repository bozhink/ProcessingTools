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
        [HttpGet]
        public Task<string> GetAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "GET");
            return Task.FromResult<string>(null);
        }

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

        [HttpDelete]
        public Task<string> DeleteAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "DELETE");
            return Task.FromResult<string>(null);
        }
    }
}
