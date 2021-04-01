// <copyright file="EchoController.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Api.Tagger.Controllers
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ProcessingTools.Common.Constants;

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
        public async Task<HttpResponseMessage> GetAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "GET");

            string content = null;

            return await BuildResponse(content, this.Request).ConfigureAwait(false);
        }

        /// <summary>
        /// Respond on POST verb.
        /// </summary>
        /// <returns>Request content as string response.</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> PostAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "POST");

            string content = await GetContentFromRequest(this.Request).ConfigureAwait(false);

            return await BuildResponse(content, this.Request).ConfigureAwait(false);
        }

        /// <summary>
        /// Respond on PATCH verb.
        /// </summary>
        /// <returns>Request content as string response.</returns>
        [HttpPatch]
        public async Task<HttpResponseMessage> PatchAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "PATCH");

            string content = await GetContentFromRequest(this.Request).ConfigureAwait(false);

            return await BuildResponse(content, this.Request).ConfigureAwait(false);
        }

        /// <summary>
        /// Respond on HEAD verb.
        /// </summary>
        /// <returns>Request content as string response.</returns>
        [HttpHead]
        public async Task<HttpResponseMessage> HeadAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "HEAD");

            string content = await GetContentFromRequest(this.Request).ConfigureAwait(false);

            return await BuildResponse(content, this.Request).ConfigureAwait(false);
        }

        /// <summary>
        /// Respond on DELETE verb.
        /// </summary>
        /// <returns>Null as string.</returns>
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync()
        {
            this.Response.Headers.Add("X-ECHO-VERB", "DELETE");

            string content = null;

            return await BuildResponse(content, this.Request).ConfigureAwait(false);
        }

        private static async Task<string> GetContentFromRequest(HttpRequest request)
        {
            string content;

            using (var tr = new StreamReader(request.Body))
            {
                content = await tr.ReadToEndAsync().ConfigureAwait(false);
            }

            return content;
        }

        private static Task<HttpResponseMessage> BuildResponse(string content, HttpRequest request)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Defaults.Encoding, request.ContentType),
            };

            return Task.FromResult<HttpResponseMessage>(response);
        }
    }
}
