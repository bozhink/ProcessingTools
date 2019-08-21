// <copyright file="EnumerableXmlHarvesterCore{TModel}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Xml;

    /// <summary>
    /// Default implementation of <see cref="IEnumerableXmlHarvesterCore{TModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public class EnumerableXmlHarvesterCore<TModel> : IEnumerableXmlHarvesterCore<TModel>
    {
        private readonly IXmlContextWrapper contextWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableXmlHarvesterCore{TModel}"/> class.
        /// </summary>
        /// <param name="contextWrapper">Context wrapper to generate <see cref="XmlDocument"/>.</param>
        public EnumerableXmlHarvesterCore(IXmlContextWrapper contextWrapper)
        {
            this.contextWrapper = contextWrapper ?? throw new ArgumentNullException(nameof(contextWrapper));
        }

        /// <inheritdoc/>
        public Task<IList<TModel>> HarvestAsync(XmlNode context, Func<XmlDocument, IList<TModel>> action)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var document = this.contextWrapper.Create(context);
            if (document == null)
            {
                return Task.FromResult<IList<TModel>>(Array.Empty<TModel>());
            }

            return Task.Run<IList<TModel>>(() => action(document));
        }

        /// <inheritdoc/>
        public Task<IList<TModel>> HarvestAsync(XmlNode context, Func<XmlDocument, Task<IList<TModel>>> actionAsync)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (actionAsync == null)
            {
                throw new ArgumentNullException(nameof(actionAsync));
            }

            var document = this.contextWrapper.Create(context);
            if (document == null)
            {
                return Task.FromResult<IList<TModel>>(Array.Empty<TModel>());
            }

            return actionAsync(document);
        }
    }
}
