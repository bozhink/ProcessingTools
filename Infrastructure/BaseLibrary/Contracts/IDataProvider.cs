namespace ProcessingTools.BaseLibrary.Contracts
{
    public interface IDataProvider
    {
        string Xml { get; set; }

        void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, string tagName, bool caseSensitive = false);
    }
}
