namespace ProcessingTools.Base
{
    public interface IDataProvider : IBase
    {
        void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, TagContent tag, bool caseSensitive = false);

        void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, string tagName);
    }
}
