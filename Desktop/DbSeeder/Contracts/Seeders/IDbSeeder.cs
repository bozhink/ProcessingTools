namespace ProcessingTools.DbSeeder.Contracts.Seeders
{
    using System.Threading.Tasks;

    public interface IDbSeeder
    {
        Task Seed();
    }
}
