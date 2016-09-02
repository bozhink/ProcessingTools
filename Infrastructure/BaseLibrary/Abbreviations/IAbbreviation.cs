namespace ProcessingTools.BaseLibrary.Abbreviations
{
    internal interface IAbbreviation
    {
        string Content { get; }

        string ContentType { get; }

        string Definition { get; }

        string ReplacePattern { get; }

        string SearchPattern { get; }
    }
}
