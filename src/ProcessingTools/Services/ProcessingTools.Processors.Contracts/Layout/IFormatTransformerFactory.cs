// <copyright file="IFormatTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Layout
{
    using ProcessingTools.Contracts.Xml;

    /// <summary>
    /// Format transformer factory.
    /// </summary>
    public interface IFormatTransformerFactory
    {
        /// <summary>
        /// Get format to NLM transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetFormatToNlmTransformer();

        /// <summary>
        /// Get format to System transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetFormatToSystemTransformer();

        /// <summary>
        /// Get NLM initial format transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetNlmInitialFormatTransformer();

        /// <summary>
        /// Get System initial format transformer.
        /// </summary>
        /// <returns><see cref="IXmlTransformer"/></returns>
        IXmlTransformer GetSystemInitialFormatTransformer();
    }
}
