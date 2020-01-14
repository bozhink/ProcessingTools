// <copyright file="Institution.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Bio.Biorepositories
{
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Data.Models.Bio.Biorepositories;

    /// <summary>
    /// Institution.
    /// </summary>
    public class Institution : MongoDataModel, IInstitutionDataModel
    {
        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string AdditionalInstitutionNames { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CitesPermitNumber { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string CoolUri { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string DateHerbariumWasFounded { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string DescriptionOfInstitution { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string GeographicCoverageOfHerbarium { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string IncorporatedHerbaria { get; set; }

        /// <inheritdoc/>
        public IndexHerbariorumRecordType IndexHerbariorumRecord { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionCode { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionName { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionType { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionalDiscipline { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionalGovernance { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string InstitutionalLsid { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string NumberOfSpecimensInHerbarium { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string PrimaryContact { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string SerialsPublishedByInstitution { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string StatusOfInstitution { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string TaxonomicCoverageOfHerbarium { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string UrlForInstitutionalSpecimenCatalog { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string UrlForInstitutionalWebservices { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string UrlForMainInstitutionalWebsite { get; set; }

        /// <inheritdoc/>
        [BsonIgnoreIfDefault]
        public string Url { get; set; }
    }
}
