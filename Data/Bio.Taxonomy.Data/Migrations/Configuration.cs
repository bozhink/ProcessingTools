namespace ProcessingTools.Bio.Taxonomy.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<TaxonomyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ProcessingTools.Bio.Taxonomy.Data.TaxonomyDbContext";
        }

        protected override void Seed(TaxonomyDbContext context)
        {
            var ranks = ParseRanksDataFile();

            if (ranks.Length < 1)
            {
                throw new ApplicationException("Ranks data file is empty or invalid.");
            }

            ImportRanksToDatabase(context, ranks);
        }

        private static void ImportRanksToDatabase(TaxonomyDbContext context, string[] ranks)
        {
            var ranksValues = new HashSet<string>(ranks)
                .Select(r => new TaxonRank
                {
                    Name = r
                })
                .ToArray();

            context.TaxonRanks.AddOrUpdate(ranksValues);
            context.SaveChanges();
        }

        private static string[] ParseRanksDataFile()
        {
            var appConfigReader = new AppSettingsReader();

            string ranksFilePath = appConfigReader.GetValue("RanksDataFile", typeof(string)).ToString();

            var ranks = File.ReadAllLines(ranksFilePath);
            return ranks;
        }
    }
}