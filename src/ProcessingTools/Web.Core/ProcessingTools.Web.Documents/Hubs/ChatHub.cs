// <copyright file="ChatHub.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Hubs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// Chat hub.
    /// </summary>
    public class ChatHub : Hub
    {
        /// <summary>
        /// Hello action.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Hello()
        {
            await this.Clients.All.SendAsync(nameof(this.Hello)).ConfigureAwait(false);
        }

        /// <summary>
        /// Send action.
        /// </summary>
        /// <param name="name">Name of the sender.</param>
        /// <param name="message">Message to send.</param>
        /// <returns>Task.</returns>
        public async Task Send(string name, string message)
        {
            DateTime now = DateTime.Now;
            await this.Clients.All.SendAsync(nameof(this.Send), now.ToString("yyyy-MM-dd hh:mm:ss.fff"), name, message).ConfigureAwait(false);
        }
    }
}
