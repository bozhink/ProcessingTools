namespace ProcessingTools.DbSeeder.Contracts.Core
{
    using System.Threading.Tasks;

    public interface IEngine
    {
        Task Run(string[] args);
    }
}
