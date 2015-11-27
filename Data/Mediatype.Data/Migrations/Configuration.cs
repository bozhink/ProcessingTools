namespace ProcessingTools.Mediatype.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Data;
    using System.Configuration;
    using System.IO;
    using Newtonsoft.Json;
    using Seed.Models;
    using System;
    using System.Linq;
    using Models;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<MediatypesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MediatypesDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var appConfigReader = new AppSettingsReader();

            string jsonFilePath = appConfigReader.GetValue("MediatypeDataJsonFilePath", typeof(string)).ToString();
            string jsonString = File.ReadAllText(jsonFilePath);

            var mediatypesJson = JsonConvert.DeserializeObject<ExtensionJson[]>(jsonString);

            if (mediatypesJson.Length < 1)
            {
                throw new ApplicationException("Mediatype data json file is empty or invalid.");
            }

            {
                var mimeTypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeType));
                context.MimeTypes.AddOrUpdate(mimeTypesNames
                    .Select(m => new MimeType
                    {
                        Name = m
                    })
                    .ToArray());

                var mimeSubtypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeSubtype));
                context.MimeSubtypes.AddOrUpdate(mimeSubtypesNames
                    .Select(s => new MimeSubtype
                    {
                        Name = s
                    })
                    .ToArray());

                var fileExtension = new HashSet<string>(mediatypesJson.Select(e => e.Extension));
                context.FileExtensions.AddOrUpdate(fileExtension
                    .Select(e => new FileExtension
                    {
                        Name = e
                    })
                    .ToArray());

                context.SaveChanges();
            }

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

            var mimeTypePairs = new HashSet<MimeTypePair>(context.MimeTypePairs.ToList());
            var extensions = context.FileExtensions.ToList();

            foreach (var e in extensions)
            {
                e.MimeTypePairs.Add(mimeTypePairs
                    .FirstOrDefault(t =>
                    {
                        var mediatype = mediatypesJson.FirstOrDefault(m => m.Extension == e.Name);
                        return (t.MimeType.Name == mediatype.MimeType) && (t.MimeSubtype.Name == mediatype.MimeSubtype);
                    }));
            }

            context.FileExtensions.AddOrUpdate(extensions.ToArray());

            context.SaveChanges();
        }
    }
}