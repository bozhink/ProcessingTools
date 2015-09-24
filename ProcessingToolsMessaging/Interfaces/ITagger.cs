namespace ProcessingTools
{
    public interface ITagger
    {
        void Tag();

        void Tag(IXPathProvider xpathProvider);
    }
}
