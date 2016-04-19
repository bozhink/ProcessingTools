namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data.Seed.Contracts;

    public class DataDbSeeder : IDataDbSeeder
    {
        private IDataSeeder seeder;

        public DataDbSeeder(IDataSeeder seeder)
        {
            if (seeder == null)
            {
                throw new ArgumentNullException(nameof(seeder));
            }

            this.seeder = seeder;
        }

        public async Task Seed()
        {
            await this.seeder.Init();
            await this.seeder.Seed();
        }
    }
}