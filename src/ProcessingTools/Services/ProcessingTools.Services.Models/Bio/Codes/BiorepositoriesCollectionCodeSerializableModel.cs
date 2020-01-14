// <copyright file="BiorepositoriesCollectionCodeSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Codes
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Biorepositories collection code serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesCollectionCodeSerializableModel : NamedContentSerializableModel
    {
        /// <summary>
        /// Gets or sets the @content-type.
        /// </summary>
        [XmlAttribute(AttributeNames.ContentType)]
        public override string ContentType
        {
            get => AttributeValues.BiorepositoriesCollectionCodeContentType;

            set => _ = value;
        }

        /// <summary>
        /// Gets or sets @xlink:title.
        /// </summary>
        [XmlAttribute(AttributeNames.XLinkTitle, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string XLinkTitle { get; set; }
    }
}
