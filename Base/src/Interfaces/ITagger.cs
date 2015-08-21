namespace ProcessingTools.Base
{
    public interface ITagger
    {
        void Tag();

        void Tag(IXPathProvider xpathProvider);
    }
}
