// <copyright file="IXQueryTransformerFactory.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Xml
{
    public interface IXQueryTransformerFactory
    {
        IXQueryTransformer CreateTransformer(string xqueryFileName);
    }
}
