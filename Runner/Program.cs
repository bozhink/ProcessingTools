namespace Runner
{
    using ProcessingTools.MimeResolver.Context;
    using ProcessingTools.MimeResolver.Migrations;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
