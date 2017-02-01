namespace ProcessingTools.Processors.Contracts.References
{
    using ProcessingTools.Contracts;

    public interface IReferencesTagger : IXmlContextTagger
    {
        string ReferencesGetReferencesXmlPath { get;  set; }
    }
}
