namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.MediaType.Data.Entity.Contracts;
    using ProcessingTools.MediaType.Data.Seed.Contracts;

    public class MediaTypesDbSeeder : IMediaTypesDbSeeder
    {
        private readonly IMediaTypeDataInitializer initializer;
        private readonly IMediaTypeDataSeeder seeder;

        public MediaTypesDbSeeder(IMediaTypeDataInitializer initializer, IMediaTypeDataSeeder seeder)
        {
            if (initializer == null)
            {
                throw new ArgumentNullException(nameof(initializer));
            }

            if (seeder == null)
            {
                throw new ArgumentNullException(nameof(seeder));
            }

            this.initializer = initializer;
            this.seeder = seeder;
        }

        public async Task Seed()
        {
            await this.initializer.Initialize();
            await this.seeder.Seed();
        }
    }
}