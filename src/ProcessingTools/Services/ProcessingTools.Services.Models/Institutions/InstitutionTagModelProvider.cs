// <copyright file="InstitutionTagModelProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;
using System.Xml;
using ProcessingTools.Common.Constants.Schema;
using ProcessingTools.Contracts.Services.Models.Institutions;

namespace ProcessingTools.Services.Models.Institutions
{
    /// <summary>
    /// Institution tag model provider.
    /// </summary>
    public class InstitutionTagModelProvider : IInstitutionTagModelProvider
    {
        /// <inheritdoc/>
        public Func<XmlDocument, XmlElement> TagModel => document =>
        {
            var tagModel = document.CreateElement(ElementNames.NamedContent);
            tagModel.SetAttribute(AttributeNames.ContentType, ContentTypes.InstitutionContentType);

            return tagModel;
        };
    }
}
