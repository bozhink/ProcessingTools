// <copyright file="BiorepositoriesCollectionSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Codes
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Biorepositories collection serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesCollectionSerializableModel : NamedContentSerializableModel
    {
        /// <summary>
        /// Gets or sets @content-type.
        /// </summary>
        [XmlAttribute(AttributeNames.ContentType)]
        public override string ContentType
        {
            get => AttributeValues.BiorepositoriesCollectionContentType;

            set => _ = value;
        }
    }
}
