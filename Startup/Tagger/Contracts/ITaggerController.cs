namespace ProcessingTools.MainProgram.Contracts
{
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts.Log;

    public interface ITaggerController
    {
        Task Run(XmlNode context, ProgramSettings settings, ILogger logger);
    }
}