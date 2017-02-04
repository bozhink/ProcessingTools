namespace ProcessingTools.Tagger.Commands.Contracts
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Enumerations;

    public interface IProgramSettings
    {
        SchemaType ArticleSchemaType { get; set; }

        ICollection<Type> CalledCommands { get; }

        bool ExpandLowerTaxa { get; }

        bool ExtractHigherTaxa { get; }

        bool ExtractLowerTaxa { get; }

        bool ExtractTaxa { get; }

        IList<string> FileNames { get; }

        bool FormatTreat { get; }

        bool InitialFormat { get; }

        bool MergeInputFiles { get; }

        string OutputFileName { get; set; }

        bool ParseCoordinates { get; }

        bool ParseHigherAboveGenus { get; }

        bool ParseHigherBySuffix { get; }

        bool ParseHigherTaxa { get; }

        bool ParseHigherWithAphia { get; }

        bool ParseHigherWithCoL { get; }

        bool ParseHigherWithGbif { get; }

        bool ParseLowerTaxa { get; }

        bool ParseReferences { get; }

        bool ParseTreatmentMetaWithAphia { get; }

        bool ParseTreatmentMetaWithCol { get; }

        bool ParseTreatmentMetaWithGbif { get; }

        bool QueryReplace { get; }

        bool ResolveMediaTypes { get; }

        bool RunXslTransform { get; }

        bool SplitDocument { get; }

        bool TagAbbreviations { get; }

        bool TagCodes { get; }

        bool TagCoordinates { get; }

        bool TagDoi { get; }

        bool TagEnvironmentTerms { get; }

        bool TagEnvironmentTermsWithExtract { get; }

        bool TagFloats { get; }

        bool TagHigherTaxa { get; }

        bool TagLowerTaxa { get; }

        bool TagReferences { get; }

        bool TagTableFn { get; }

        bool TagWebLinks { get; }

        bool UntagSplit { get; }

        bool ZoobankCloneJson { get; }

        bool ZoobankCloneXml { get; }

        bool ZoobankGenerateRegistrationXml { get; }
    }
}
