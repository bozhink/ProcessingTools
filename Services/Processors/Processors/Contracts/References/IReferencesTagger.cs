namespace ProcessingTools.Processors.Contracts.References
{
    using ProcessingTools.Contracts;

    public interface IReferencesTagger : IGenericXmlContextTagger<object>
    {
        string ReferencesGetReferencesXmlPath { get;  set; }
    }
}
