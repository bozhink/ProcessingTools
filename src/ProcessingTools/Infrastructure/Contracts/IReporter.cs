namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IReporter
    {
        void AppendContent(string content);

        Task MakeReport();
    }
}
