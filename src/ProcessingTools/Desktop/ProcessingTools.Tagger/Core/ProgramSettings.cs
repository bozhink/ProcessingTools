// <copyright file="ProgramSettings.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Core
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Tagger.Contracts;

    /// <summary>
    /// Program settings.
    /// </summary>
    public class ProgramSettings : IProgramSettings
    {
        private SchemaType articleSchemaType;
        private bool articleSchemaTypeStyleIsLockedForModification;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramSettings"/> class.
        /// </summary>
        public ProgramSettings()
        {
            this.FileNames = new List<string>();
            this.CalledCommands = new List<Type>();

            this.articleSchemaType = SchemaType.System;
            this.articleSchemaTypeStyleIsLockedForModification = false;

            this.ExtractHigherTaxa = false;
            this.ExtractLowerTaxa = false;
            this.ExtractTaxa = false;
            this.InitialFormat = false;
            this.FormatTreat = false;
            this.ParseCoordinates = false;
            this.ParseReferences = false;
            this.ParseTreatmentMetaWithAphia = false;
            this.ParseTreatmentMetaWithCol = false;
            this.ParseTreatmentMetaWithGbif = false;
            this.QueryReplace = false;
            this.ParseHigherAboveGenus = false;
            this.ParseHigherBySuffix = false;
            this.ParseHigherWithAphia = false;
            this.ParseHigherWithCoL = false;
            this.ParseHigherWithGbif = false;
            this.ResolveMediaTypes = false;
            this.TagAbbreviations = false;
            this.TagCodes = false;
            this.TagCoordinates = false;
            this.TagDoi = false;
            this.TagEnvironmentTerms = false;
            this.TagEnvironmentTermsWithExtract = false;
            this.TagFloats = false;
            this.TagReferences = false;
            this.TagTableFn = false;
            this.TagWebLinks = false;
            this.TagLowerTaxa = false;
            this.TagHigherTaxa = false;
            this.ParseLowerTaxa = false;
            this.ParseHigherTaxa = false;
            this.ExpandLowerTaxa = false;
            this.UntagSplit = false;
            this.RunXslTransform = false;
            this.ZoobankCloneJson = false;
            this.ZoobankCloneXml = false;
            this.ZoobankGenerateRegistrationXml = false;
            this.MergeInputFiles = false;
            this.SplitDocument = false;
        }

        /// <inheritdoc/>
        public SchemaType ArticleSchemaType
        {
            get
            {
                this.articleSchemaTypeStyleIsLockedForModification = true;
                return this.articleSchemaType;
            }

            set
            {
                if (!this.articleSchemaTypeStyleIsLockedForModification)
                {
                    this.articleSchemaType = value;
                }

                this.articleSchemaTypeStyleIsLockedForModification = true;
            }
        }

        /// <inheritdoc/>
        public ICollection<Type> CalledCommands { get; private set; }

        /// <inheritdoc/>
        public bool ExpandLowerTaxa { get; set; }

        /// <inheritdoc/>
        public bool ExtractHigherTaxa { get; set; }

        /// <inheritdoc/>
        public bool ExtractLowerTaxa { get; set; }

        /// <inheritdoc/>
        public bool ExtractTaxa { get; set; }

        /// <inheritdoc/>
        public IList<string> FileNames { get; private set; }

        /// <inheritdoc/>
        public bool FormatTreat { get; set; }

        /// <inheritdoc/>
        public bool InitialFormat { get; set; }

        /// <inheritdoc/>
        public bool MergeInputFiles { get; set; }

        /// <inheritdoc/>
        public string OutputFileName { get; set; }

        /// <inheritdoc/>
        public bool ParseCoordinates { get; set; }

        /// <inheritdoc/>
        public bool ParseHigherAboveGenus { get; set; }

        /// <inheritdoc/>
        public bool ParseHigherBySuffix { get; set; }

        /// <inheritdoc/>
        public bool ParseHigherTaxa { get; set; }

        /// <inheritdoc/>
        public bool ParseHigherWithAphia { get; set; }

        /// <inheritdoc/>
        public bool ParseHigherWithCoL { get; set; }

        /// <inheritdoc/>
        public bool ParseHigherWithGbif { get; set; }

        /// <inheritdoc/>
        public bool ParseLowerTaxa { get; set; }

        /// <inheritdoc/>
        public bool ParseReferences { get; set; }

        /// <inheritdoc/>
        public bool ParseTreatmentMetaWithAphia { get; set; }

        /// <inheritdoc/>
        public bool ParseTreatmentMetaWithCol { get; set; }

        /// <inheritdoc/>
        public bool ParseTreatmentMetaWithGbif { get; set; }

        /// <inheritdoc/>
        public bool QueryReplace { get; set; }

        /// <inheritdoc/>
        public bool ResolveMediaTypes { get; set; }

        /// <inheritdoc/>
        public bool RunXslTransform { get; set; }

        /// <inheritdoc/>
        public bool SplitDocument { get; set; }

        /// <inheritdoc/>
        public bool TagAbbreviations { get; set; }

        /// <inheritdoc/>
        public bool TagCodes { get; set; }

        /// <inheritdoc/>
        public bool TagCoordinates { get; set; }

        /// <inheritdoc/>
        public bool TagDoi { get; set; }

        /// <inheritdoc/>
        public bool TagEnvironmentTerms { get; set; }

        /// <inheritdoc/>
        public bool TagEnvironmentTermsWithExtract { get; set; }

        /// <inheritdoc/>
        public bool TagFloats { get; set; }

        /// <inheritdoc/>
        public bool TagHigherTaxa { get; set; }

        /// <inheritdoc/>
        public bool TagLowerTaxa { get; set; }

        /// <inheritdoc/>
        public bool TagReferences { get; set; }

        /// <inheritdoc/>
        public bool TagTableFn { get; set; }

        /// <inheritdoc/>
        public bool TagWebLinks { get; set; }

        /// <inheritdoc/>
        public bool UntagSplit { get; set; }

        /// <inheritdoc/>
        public bool ZoobankCloneJson { get; set; }

        /// <inheritdoc/>
        public bool ZoobankCloneXml { get; set; }

        /// <inheritdoc/>
        public bool ZoobankGenerateRegistrationXml { get; set; }
    }
}
