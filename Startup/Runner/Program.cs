namespace Runner
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using ProcessingTools.MediaType.Data;
    using ProcessingTools.MediaType.Data.Migrations;
    using ProcessingTools.MediaType.Data.Seed.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            var appConfigReader = new AppSettingsReader();

            string jsonFilePath = appConfigReader.GetValue("MediaTypeDataJsonFilePath", typeof(string)).ToString();
            string jsonString = File.ReadAllText(jsonFilePath);

            var json = JsonConvert.DeserializeObject<ExtensionJson[]>(jsonString);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaTypesDbContext, Configuration>());

            var db = new MediaTypesDbContext();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);

            var fileExtensionsInDb = new HashSet<string>(db.FileExtensions
                .Select(e => e.Name)
                .ToList());

            var fileExtensionsInJson = new HashSet<string>(json.Select(j => j.Extension));

            var mimeTypesInDb = new HashSet<string>(db.MimeTypes
                .Select(m => m.Name)
                .ToList());

            var mimeTypesInJson = new HashSet<string>(json.Select(j => j.MimeType));

            var mimeSubtyesInDb = new HashSet<string>(db.MimeSubtypes
                .Select(m => m.Name)
                .ToList());

            var mimeSubtypesInJson = new HashSet<string>(json.Select(j => j.MimeSubtype));

            Console.WriteLine(fileExtensionsInJson.Count);
            Console.WriteLine(mimeTypesInJson.Count);
            Console.WriteLine(mimeSubtypesInJson.Count);

            db.SaveChanges();
            db.Dispose();
        }
    }
}
