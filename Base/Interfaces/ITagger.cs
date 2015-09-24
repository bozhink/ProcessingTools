namespace ProcessingTools.BaseLibrary
{
    public interface ITagger
    {
        void Tag();

        void Tag(IXPathProvider xpathProvider);
    }
}
