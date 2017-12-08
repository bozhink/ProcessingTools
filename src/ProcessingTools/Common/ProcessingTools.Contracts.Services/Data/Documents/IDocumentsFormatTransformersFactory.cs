// <copyright file="IDocumentsFormatTransformersFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using ProcessingTools.Contracts.Processors;

    public interface IDocumentsFormatTransformersFactory
    {
        IXmlTransformer GetFormatXmlToHtmlTransformer();

        IXmlTransformer GetFormatHtmlToXmlTransformer();
    }
}
