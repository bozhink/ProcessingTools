namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.MediaType.Data.Seed.Contracts;

    public class MediaTypesDbSeeder : IMediaTypesDbSeeder
    {
        private readonly IMediaTypeDataSeeder seeder;

        public MediaTypesDbSeeder(IMediaTypeDataSeeder seeder)
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