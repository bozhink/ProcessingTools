namespace ProcessingTools.DbSeeder.Contracts
{
    using System.Threading.Tasks;

    public interface IDbSeeder
    {
        Task Seed();
    }
}