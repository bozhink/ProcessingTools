// <copyright file="IProgramSettings.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tagger.Contracts
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Commands.Models;

    /// <summary>
    /// Program settings.
    /// </summary>
    public interface IProgramSettings : ICommandSettings
    {
        /// <summary>
        /// Gets or sets the article schema type.
        /// </summary>
        SchemaType ArticleSchemaType { get; set; }

        /// <summary>
        /// Gets the collection of called commands.
        /// </summary>
        ICollection<Type> CalledCommands { get; }

        /// <summary>
        /// Gets a value indicating whether lower taxa must be expanded.
        /// </summary>
        bool ExpandLowerTaxa { get; }

        /// <summary>
        /// Gets a value indicating whether taxon treatment must be formated.
        /// </summary>
        bool FormatTreat { get; }

        /// <summary>
        /// Gets a value indicating whether initial format must be performed.
        /// </summary>
        bool InitialFormat { get; }

        /// <summary>
        /// Gets a value indicating whether all input files must be merged into single file.
        /// </summary>
        bool MergeInputFiles { get; }

        /// <summary>
        /// Gets a value indicating whether coordinates must be parsed.
        /// </summary>
        bool ParseCoordinates { get; }

        /// <summary>
        /// Gets a value indicating whether non-resolved higher taxon ranks must be set to value "above-genus".
        /// </summary>
        bool ParseHigherAboveGenus { get; }

        /// <summary>
        /// Gets a value indicating whether non-resolved higher taxon ranks must be resolved by the suffix of the taxon name.
        /// </summary>
        bool ParseHigherBySuffix { get; }

        /// <summary>
        /// Gets a value indicating whether non-resolved higher taxon ranks must be parsed.
        /// </summary>
        bool ParseHigherTaxa { get; }

        /// <summary>
        /// Gets a value indicating whether non-resolved higher taxon ranks must be parsed with APHIA service.
        /// </summary>
        bool ParseHigherWithAphia { get; }

        /// <summary>
        /// Gets a value indicating whether non-resolved higher taxon ranks must be parsed with CoL service.
        /// </summary>
        bool ParseHigherWithCoL { get; }

        /// <summary>
        /// Gets a value indicating whether non-resolved higher taxon ranks must be parsed with GBIF service.
        /// </summary>
        bool ParseHigherWithGbif { get; }

        /// <summary>
        /// Gets a value indicating whether lower taxon ranks must be parsed.
        /// </summary>
        bool ParseLowerTaxa { get; }

        /// <summary>
        /// Gets a value indicating whether bibliographic references must be parsed.
        /// </summary>
        bool ParseReferences { get; }

        /// <summary>
        /// Gets a value indicating whether the classification in taxon treatments must be parsed with APHIA service.
        /// </summary>
        bool ParseTreatmentMetaWithAphia { get; }

        /// <summary>
        /// Gets a value indicating whether the classification in taxon treatments must be parsed with CoL service.
        /// </summary>
        bool ParseTreatmentMetaWithCol { get; }

        /// <summary>
        /// Gets a value indicating whether the classification in taxon treatments must be parsed with GBIF service.
        /// </summary>
        bool ParseTreatmentMetaWithGbif { get; }

        /// <summary>
        /// Gets a value indicating whether replace with query file must be performed.
        /// </summary>
        bool QueryReplace { get; }

        /// <summary>
        /// Gets a value indicating whether media-types of the supplemental materials must be parsed.
        /// </summary>
        bool ResolveMediaTypes { get; }

        /// <summary>
        /// Gets a value indicating whether XSL transform with external file must be performed.
        /// </summary>
        bool RunXslTransform { get; }

        /// <summary>
        /// Gets a value indicating whether output document must be split by 'article' tag on first-child level.
        /// </summary>
        bool SplitDocument { get; }

        /// <summary>
        /// Gets a value indicating whether abbreviations must be tagged.
        /// </summary>
        bool TagAbbreviations { get; }

        /// <summary>
        /// Gets a value indicating whether specimen codes must be tagged.
        /// </summary>
        bool TagCodes { get; }

        /// <summary>
        /// Gets a value indicating whether geographic coordinates must be tagged.
        /// </summary>
        bool TagCoordinates { get; }

        /// <summary>
        /// Gets a value indicating whether DOI references must be tagged.
        /// </summary>
        bool TagDoi { get; }

        /// <summary>
        /// Gets a value indicating whether environment terms must be tagged.
        /// </summary>
        bool TagEnvironmentTerms { get; }

        /// <summary>
        /// Gets a value indicating whether environment terms must be tagged with EXTRACT service.
        /// </summary>
        bool TagEnvironmentTermsWithExtract { get; }

        /// <summary>
        /// Gets a value indicating whether floating objects must be tagged.
        /// </summary>
        bool TagFloats { get; }

        /// <summary>
        /// Gets a value indicating whether higher taxon names must be tagged.
        /// </summary>
        bool TagHigherTaxa { get; }

        /// <summary>
        /// Gets a value indicating whether lower taxon names must be tagged.
        /// </summary>
        bool TagLowerTaxa { get; }

        /// <summary>
        /// Gets a value indicating whether bibliographic cross-citations must be tagged.
        /// </summary>
        bool TagReferences { get; }

        /// <summary>
        /// Gets a value indicating whether table footnote references must be tagged.
        /// </summary>
        bool TagTableFn { get; }

        /// <summary>
        /// Gets a value indicating whether URL references must be tagged.
        /// </summary>
        bool TagWebLinks { get; }

        /// <summary>
        /// Gets a value indicating whether all taxon-name-part tags must be removed.
        /// </summary>
        bool UntagSplit { get; }

        /// <summary>
        /// Gets a value indicating whether to set ZooBank references from external JSON file into the article placeholders.
        /// </summary>
        bool ZoobankCloneJson { get; }

        /// <summary>
        /// Gets a value indicating whether to set ZooBank references from external XML file into the article placeholders.
        /// </summary>
        bool ZoobankCloneXml { get; }

        /// <summary>
        /// Gets a value indicating whether to generate XML file for registration in ZooBank.
        /// </summary>
        bool ZoobankGenerateRegistrationXml { get; }
    }
}
