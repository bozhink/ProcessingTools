namespace ProcessingTools.Tagger.Contracts
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface ITaggerController
    {
        Task Run(XmlNode context, IProgramSettings settings);
    }
}
