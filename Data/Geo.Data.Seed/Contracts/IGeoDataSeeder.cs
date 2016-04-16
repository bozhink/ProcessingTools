namespace ProcessingTools.Geo.Data.Seed.Contracts
{
    using System.Threading.Tasks;

    public interface IGeoDataSeeder
    {
        Task Seed();
    }
}