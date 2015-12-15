namespace ProcessingTools.BaseLibrary.Contracts
{
    public interface ITagContent
    {
        string Attributes { get; set; }

        string CloseTag { get; }

        string FullTag { get; set; }

        bool IsClosingTag { get; set; }

        string Name { get; set; }

        string OpenTag { get; }
    }
}