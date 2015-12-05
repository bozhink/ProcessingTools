namespace ProcessingTools.MediaType.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Data;
    using Models;
    using Newtonsoft.Json;
    using Seed.Models;

    public sealed class Configuration : DbMigrationsConfiguration<MediaTypesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ProcessingTools.MediaType.Data.MediaTypesDbContext";
        }

        protected override void Seed(MediaTypesDbContext context)
        {
            if (context.FileExtensions.Count() > 0)
            {
                return;
            }

            ExtensionJson[] mediatypesJson = ParseDataJsonFile();

            if (mediatypesJson.Length < 1)
            {
                throw new ApplicationException("Mediatype data json file is empty or invalid.");
            }

            ImportMimeTypesToDatabase(context, mediatypesJson);
            context.Dispose();

            context = new MediaTypesDbContext();
            ImportMimeSubtypesToDataBase(context, mediatypesJson);
            context.Dispose();

            context = new MediaTypesDbContext();
            ImportFileExtensionsToDatabase(context, mediatypesJson);
            context.Dispose();

            context = new MediaTypesDbContext();
            CreateMediaTypePairsInDatabase(context, mediatypesJson);
            context.Dispose();

            context = new MediaTypesDbContext();
            ConnectMediaTypePairsToFileExtensions(context, mediatypesJson);
        }

        private static void ConnectMediaTypePairsToFileExtensions(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeTypePairs = new HashSet<MimeTypePair>(context.MimeTypePairs.ToList());
            var extensions = context.FileExtensions.ToList();

            foreach (var e in extensions)
            {
                e.MimeTypePairs.Add(mimeTypePairs
                    .FirstOrDefault(t =>
                    {
                        var mediatype = mediatypesJson.FirstOrDefault(m => m.Extension == e.Name);
                        return (t.MimeType.Name == mediatype.MimeType) &&
                               (t.MimeSubtype.Name == mediatype.MimeSubtype);
                    }));
            }

            context.FileExtensions.AddOrUpdate(extensions.ToArray());

            context.SaveChanges();
        }

        private static void CreateMediaTypePairsInDatabase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeTypes = new HashSet<MimeType>(context.MimeTypes.ToList());
            var mimeSubtypes = new HashSet<MimeSubtype>(context.MimeSubtypes.ToList());

            context.MimeTypePairs.AddOrUpdate(mediatypesJson
                .Select(p => new MimeTypePair
                {
                    MimeType = mimeTypes.FirstOrDefault(m => m.Name == p.MimeType),
                    MimeSubtype = mimeSubtypes.FirstOrDefault(s => s.Name == p.MimeSubtype)
                })
                .ToArray());

            context.SaveChanges();
        }

        private static void ImportFileExtensionsToDatabase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var fileExtension = new HashSet<string>(mediatypesJson.Select(e => e.Extension));

            context.FileExtensions.AddOrUpdate(fileExtension
                .Select(e => new FileExtension
                {
                    Name = e
                })
                .ToArray());

            context.SaveChanges();
        }

        private static void ImportMimeSubtypesToDataBase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeSubtypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeSubtype));

            context.MimeSubtypes.AddOrUpdate(mimeSubtypesNames
                .Select(s => new MimeSubtype
                {
                    Name = s
                })
                .ToArray());

            context.SaveChanges();
        }

        private static void ImportMimeTypesToDatabase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeTypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeType));

            context.MimeTypes.AddOrUpdate(mimeTypesNames
                .Select(m => new MimeType
                {
                    Name = m
                })
                .ToArray());

            context.SaveChanges();
        }

        private static ExtensionJson[] ParseDataJsonFile()
        {
            var appConfigReader = new AppSettingsReader();

            string jsonFilePath = appConfigReader.GetValue("MediaTypeDataJsonFilePath", typeof(string)).ToString();
            string jsonString = File.ReadAllText(jsonFilePath);

            var mediatypesJson = JsonConvert.DeserializeObject<ExtensionJson[]>(jsonString);
            return mediatypesJson?.Select(m => new ExtensionJson
            {
                Extension = m.Extension.ToLower(),
                MimeType = m.MimeType.ToLower(),
                MimeSubtype = m.MimeSubtype.ToLower(),
                Description = m.Description
            })
            .ToArray();
        }
    }
}