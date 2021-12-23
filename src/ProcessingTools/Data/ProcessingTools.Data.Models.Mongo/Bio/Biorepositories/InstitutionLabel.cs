// <copyright file="InstitutionLabel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Biorepositories
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Data.Models.Bio.Biorepositories;

    /// <summary>
    /// Institution label.
    /// </summary>
    public class InstitutionLabel : MongoDataModel, IInstitutionLabelDataModel
    {
        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CityTown { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Country { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string NameOfInstitution { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PostalZipCode { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PrimaryContact { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string StateProvince { get; set; }
    }
}
