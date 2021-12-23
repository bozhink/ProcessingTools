// <copyright file="Staff.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Biorepositories
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Contracts.Data.Models.Bio.Biorepositories;

    /// <summary>
    /// Staff.
    /// </summary>
    public class Staff : MongoDataModel, IStaffDataModel
    {
        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string AdditionalAffiliations { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string AreaOfResponsibility { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string BirthYear { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CityTown { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Country { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Email { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string FaxNumber { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string FirstName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string JobTitle { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string LastName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PhoneNumber { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PostalZipCode { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PrimaryCollection { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PrimaryInstitution { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string ResearchDiscipline { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string ResearchSpecialty { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string StaffMemberFullName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string StateProvince { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Url { get; set; }
    }
}
