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
    using Models.Json;
    using Newtonsoft.Json;

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

            context.Configuration.UseDatabaseNullSemantics = true;
            context.Configuration.ValidateOnSaveEnabled = false;

            ExtensionJson[] mediatypesJson = ParseDataJsonFile();

            if (mediatypesJson.Length < 1)
            {
                throw new ApplicationException("Mediatype data json file is empty or invalid.");
            }

            ImportMimeTypesToDatabase(context, mediatypesJson);
            ImportMimeSubtypesToDataBase(context, mediatypesJson);
            ImportFileExtensionsToDatabase(context, mediatypesJson);
            CreateMediaTypePairsInDatabase(context, mediatypesJson);
            ConnectMediaTypePairsToFileExtensions(context, mediatypesJson);
        }

        private static void ConnectMediaTypePairsToFileExtensions(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeTypePairs = new HashSet<MimeTypePair>(context.MimeTypePairs.ToList());
            var extensions = context.FileExtensions.ToList();

            foreach (var extension in extensions)
            {
                var extensionData = new HashSet<ExtensionJson>(mediatypesJson
                    .Where(m => m.Extension == extension.Name));

                var pairs = new HashSet<MimeTypePair>(extensionData.Select(d => 
                    mimeTypePairs.FirstOrDefault(p => 
                        (p.MimeType.Name == d.MimeType) && (p.MimeSubtype.Name == d.MimeSubtype))));

                foreach (var pair in pairs)
                {
                    extension.MimeTypePairs.Add(pair);
                }
            }

            context.FileExtensions.AddOrUpdate(extensions.ToArray());
            context.SaveChanges();
        }

        private static void CreateMediaTypePairsInDatabase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeTypes = new HashSet<MimeType>(context.MimeTypes.ToList());
            var mimeSubtypes = new HashSet<MimeSubtype>(context.MimeSubtypes.ToList());

            var mediaTypesPairs = mediatypesJson
                .Select(p => new MimeTypePair
                {
                    MimeType = mimeTypes.FirstOrDefault(m => m.Name == p.MimeType),
                    MimeSubtype = mimeSubtypes.FirstOrDefault(s => s.Name == p.MimeSubtype)
                })
                .ToArray();

            context.MimeTypePairs.AddOrUpdate(mediaTypesPairs);
            context.SaveChanges();
        }

        private static void ImportFileExtensionsToDatabase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var fileExtension = new HashSet<string>(mediatypesJson.Select(e => e.Extension))
                .Select(e => new FileExtension
                {
                    Name = e
                })
                .ToArray();

            context.FileExtensions.AddOrUpdate(fileExtension);
            context.SaveChanges();
        }

        private static void ImportMimeSubtypesToDataBase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeSubtypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeSubtype))
                .Select(s => new MimeSubtype
                {
                    Name = s
                })
                .ToArray();

            context.MimeSubtypes.AddOrUpdate(mimeSubtypesNames);
            context.SaveChanges();
        }

        private static void ImportMimeTypesToDatabase(MediaTypesDbContext context, ExtensionJson[] mediatypesJson)
        {
            var mimeTypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeType))
                .Select(m => new MimeType
                {
                    Name = m
                })
                .ToArray();

            context.MimeTypes.AddOrUpdate(mimeTypesNames);
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