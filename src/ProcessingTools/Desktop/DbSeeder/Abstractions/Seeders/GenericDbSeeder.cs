namespace ProcessingTools.DbSeeder.Abstractions.Seeders
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public abstract class GenericDbSeeder<TInitializer, TSeeder> : IDbSeeder
        where TInitializer : IDatabaseInitializer
        where TSeeder : IDatabaseSeeder
    {
        private readonly TInitializer initializer;
        private readonly TSeeder seeder;

        public GenericDbSeeder(TInitializer initializer, TSeeder seeder)
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

        public virtual async Task Seed()
        {
            await this.initializer.Initialize();
            await this.seeder.Seed();
        }
    }
}
