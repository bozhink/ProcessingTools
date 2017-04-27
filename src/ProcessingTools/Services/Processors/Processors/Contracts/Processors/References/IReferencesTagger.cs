namespace ProcessingTools.Processors.Contracts.Processors.References
{
    using ProcessingTools.Contracts;

    public interface IReferencesTagger : IXmlContextTagger
    {
        string ReferencesGetReferencesXmlPath { get;  set; }
    }
}
