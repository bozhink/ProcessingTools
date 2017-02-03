namespace ProcessingTools.Processors.Contracts.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    public interface ITaxaStopWordsProvider
    {
        Task<IEnumerable<string>> GetStopWords(XmlNode context);
    }
}
