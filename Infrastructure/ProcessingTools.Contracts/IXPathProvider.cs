namespace ProcessingTools.Contracts
{
    public interface IXPathProvider
    {
        string SelectContentNodesXPath
        {
            get;
        }

        string SelectContentNodesXPathTemplate
        {
            get;
        }
    }
}
