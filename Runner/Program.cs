namespace Runner
{
    using System.Data.Entity;
    using ProcessingTools.MimeResolver.Context;
    using ProcessingTools.MimeResolver.Migrations;

    public class Program
    {
        public static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MimeTypesDbContext, Configuration>());

            using (var db = new MimeTypesDbContext())
            {
                db.Database.CreateIfNotExists();

                db.SaveChanges();
            }
        }
    }
}
