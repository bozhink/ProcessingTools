namespace ProcessingTools.Contracts
{
    public interface ITagger
    {
        void Tag();

        void Tag(IXPathProvider xpathProvider);
    }
}
