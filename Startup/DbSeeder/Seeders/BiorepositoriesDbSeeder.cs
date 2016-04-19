namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Biorepositories.Data;
    using ProcessingTools.Bio.Biorepositories.Data.Seed;

    public class BiorepositoriesDbSeeder : IBiorepositoriesDbSeeder
    {
        public async Task Seed()
        {
            var provider = new BiorepositoriesMongoDatabaseProvider();
            var configuration = new Configuration(provider);
            await configuration.Seed();
        }
    }
}