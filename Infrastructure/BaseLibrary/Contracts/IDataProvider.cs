namespace ProcessingTools.BaseLibrary.Contracts
{
    public interface IDataProvider : IBase
    {
        void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, string tagName, bool caseSensitive = false);
    }
}
