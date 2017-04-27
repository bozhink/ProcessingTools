namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface ICommandRunner
    {
        Task<object> Run(string commandName);
    }
}
