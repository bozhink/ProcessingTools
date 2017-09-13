namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface ICommandRunner
    {
        Task<object> RunAsync(string commandName);
    }
}
