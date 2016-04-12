namespace ProcessingTools.Common
{
    using System.Collections.Concurrent;

    public static class Dictionaries
    {
        public static readonly ConcurrentDictionary<string, string> FileNames = new ConcurrentDictionary<string, string>();
    }
}
