// <copyright file="BiorepositoriesCollectionCodeSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

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
            get
            {
                return AttributeValues.BiorepositoriesCollectionCodeContentType;
            }

            set
            {
                // Skip
            }
        }

        /// <summary>
        /// Gets or sets @xlink:title.
        /// </summary>
        [XmlAttribute(AttributeNames.XLinkTitle, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string XLinkTitle { get; set; }
    }
}
