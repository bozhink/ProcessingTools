namespace ProcessingTools.Contracts
{
    using System.Collections.Generic;

    public interface IStringDataList
    {
        IEnumerable<string> StringList { get; }

        void Clear();
    }
}
