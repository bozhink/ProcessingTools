namespace ProcessingTools.BaseLibrary.Contracts
{
    public interface IDataProvider : IBase
    {
        void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, TagContent tag, bool caseSensitive = false);
    }
}
