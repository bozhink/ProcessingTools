// <copyright file="InterceptRequestAndResponseEndpointBehavior.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.ConnectedServices.Interceptors
{
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// Intercept request and response endpoint behavior.
    /// </summary>
    public class InterceptRequestAndResponseEndpointBehavior : IEndpointBehavior
    {
        private readonly InterceptRequestAndResponseClientMessageInspector messageInspector;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptRequestAndResponseEndpointBehavior"/> class.
        /// </summary>
        public InterceptRequestAndResponseEndpointBehavior()
        {
            this.messageInspector = new InterceptRequestAndResponseClientMessageInspector();
        }

        /// <summary>
        /// Gets the intercepted request content.
        /// </summary>
        public string RequestContent => this.messageInspector.RequestContent;

        /// <summary>
        /// Gets the intercepted response content.
        /// </summary>
        public string ResponseContent => this.messageInspector.ResponseContent;

        /// <inheritdoc/>
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc/>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime?.ClientMessageInspectors.Add(this.messageInspector);
        }

        /// <inheritdoc/>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc/>
        public void Validate(ServiceEndpoint endpoint)
        {
            // Method intentionally left empty.
        }
    }
}
