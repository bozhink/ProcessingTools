namespace Runner
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.IO;

    using Newtonsoft.Json;
    using ProcessingTools.MediaType.Data;
    using ProcessingTools.MediaType.Data.Migrations;
    using ProcessingTools.MediaType.Data.Seed.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var appConfigReader = new AppSettingsReader();

            string jsonFilePath = appConfigReader.GetValue("MediaTypeDataJsonFilePath", typeof(string)).ToString();
            string jsonString = File.ReadAllText(jsonFilePath);

            var json = JsonConvert.DeserializeObject<ExtensionJson[]>(jsonString);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaTypesDbContext, Configuration>());

            var db = new MediaTypesDbContext();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);
            db.SaveChanges();
            db.Dispose();

            Console.WriteLine(timer.Elapsed);
        }
    }
}