namespace ProcessingTools.BaseLibrary
{
    public interface IXPathProvider
    {
        Config Config
        {
            get;
        }

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
