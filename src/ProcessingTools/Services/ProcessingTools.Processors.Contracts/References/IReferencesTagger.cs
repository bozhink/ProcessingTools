namespace ProcessingTools.Contracts.Processors.Processors.References
{
    using ProcessingTools.Contracts;

    public interface IReferencesTagger : IXmlContextTagger
    {
        string ReferencesGetReferencesXmlPath { get;  set; }
    }
}
