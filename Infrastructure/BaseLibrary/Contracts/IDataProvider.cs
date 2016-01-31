namespace ProcessingTools.BaseLibrary.Contracts
{
    public interface IDataProvider : IConfigurableDocument
    {
        void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, string tagName, bool caseSensitive = false);
    }
}
