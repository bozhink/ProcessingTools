// <copyright file="SerializableModelWithXLinkTypeSimple.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models
{
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using ProcessingTools.Constants.Schema;

    /// <summary>
    /// Serializable model with @xlink-type="simple".
    /// </summary>
    public class SerializableModelWithXLinkTypeSimple
    {
        /// <summary>
        /// Gets or sets @xlink-type.
        /// </summary>
        [XmlAttribute(AttributeNames.XLinkType, Form = XmlSchemaForm.Qualified, Namespace = Namespaces.XlinkNamespaceUri)]
        public string XLinkType
        {
            get
            {
                return ProcessingTools.Enumerations.Nlm.XLinkType.Simple.ToString().ToLowerInvariant();
            }

            set
            {
                // Skip
            }
        }
    }
}
