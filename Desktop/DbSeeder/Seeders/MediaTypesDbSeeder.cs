namespace ProcessingTools.DbSeeder.Seeders
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Seed.Contracts;

    public class MediaTypesDbSeeder : IMediaTypesDbSeeder
    {
        private readonly IMediatypesDataInitializer initializer;
        private readonly IMediatypesDataSeeder seeder;

        public MediaTypesDbSeeder(IMediatypesDataInitializer initializer, IMediatypesDataSeeder seeder)
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