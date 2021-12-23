// <copyright file="BiorepositoriesInstitutionSerializableModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Codes
{
    using System.Xml.Serialization;
    using ProcessingTools.Common.Constants.Schema;

    /// <summary>
    /// Biorepositories institution serializable model.
    /// </summary>
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementNames.NamedContent, Namespace = "", IsNullable = false)]
    public class BiorepositoriesInstitutionSerializableModel : NamedContentSerializableModel
    {
        /// <summary>
        /// Gets or sets the @content-type.
        /// </summary>
        [XmlAttribute(AttributeNames.ContentType)]
        public override string ContentType
        {
            get => AttributeValues.BiorepositoriesInstitutionContentType;

            set => _ = value;
        }
    }
}
