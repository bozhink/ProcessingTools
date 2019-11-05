// <copyright file="InterceptRequestAndResponseClientMessageInspector.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.ConnectedServices.Interceptors
{
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// Intercept request and response client message inspector.
    /// </summary>
    public class InterceptRequestAndResponseClientMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Gets the intercepted request content.
        /// </summary>
        public string RequestContent { get; private set; }

        /// <summary>
        /// Gets the intercepted response content.
        /// </summary>
        public string ResponseContent { get; private set; }

        /// <inheritdoc/>
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            this.ResponseContent = reply?.ToString();
        }

        /// <inheritdoc/>
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            this.RequestContent = request?.ToString();
            return request;
        }
    }
}
