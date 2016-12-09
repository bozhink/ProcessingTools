namespace ProcessingTools.Tagger.Contracts
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Types;

    public interface IProgramSettings
    {
        SchemaType ArticleSchemaType { get; set; }

        ICollection<Type> CalledControllers { get; }

        bool ExpandLowerTaxa { get; }

        bool ExtractHigherTaxa { get; }

        bool ExtractLowerTaxa { get; }

        bool ExtractTaxa { get; }

        IList<string> FileNames { get; }

        bool FormatTreat { get; }

        string HigherStructrureXpath { get; }

        bool InitialFormat { get; }

        bool ParseBySection { get; }

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

        string ReferencesGetReferencesXmlPath { get; set; }

        bool ResolveMediaTypes { get; }

        bool RunXslTransform { get; }

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

        bool ValidateTaxa { get; }

        bool ZoobankCloneJson { get; }

        bool ZoobankCloneXml { get; }

        bool ZoobankGenerateRegistrationXml { get; }
    }
}
