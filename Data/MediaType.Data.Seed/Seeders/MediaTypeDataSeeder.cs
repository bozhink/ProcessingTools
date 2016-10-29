namespace ProcessingTools.MediaType.Data.Seed
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using Newtonsoft.Json;

    using ProcessingTools.MediaType.Data.Contracts;
    using ProcessingTools.MediaType.Data.Models;

    public class MediaTypeDataSeeder : IMediaTypeDataSeeder
    {
        private const string MediaTypeDataJsonFilePathKey = "MediaTypeDataJsonFilePath";

        private readonly IMediaTypesDbContextProvider contextProvider;

        private ConcurrentQueue<Exception> exceptions;

        public MediaTypeDataSeeder(IMediaTypesDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> Seed()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            ExtensionJson[] mediatypesJson = this.ParseDataJsonFile();

            if (mediatypesJson.Length < 1)
            {
                throw new ApplicationException("Mediatype data json file is empty or invalid.");
            }

            await this.ImportMimeTypesToDatabase(mediatypesJson);
            await this.ImportMimeSubtypesToDataBase(mediatypesJson);
            await this.ImportFileExtensionsToDatabase(mediatypesJson);
            await this.CreateMediaTypePairsInDatabase(mediatypesJson);
            await this.ConnectMediaTypePairsToFileExtensions(mediatypesJson);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private ExtensionJson[] ParseDataJsonFile()
        {
            string jsonFilePath = ConfigurationManager.AppSettings[MediaTypeDataJsonFilePathKey];

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

        private async Task ConnectMediaTypePairsToFileExtensions(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    context.Configuration.UseDatabaseNullSemantics = true;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    var mimeTypePairs = context.MimeTypePairs.ToList();
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
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task CreateMediaTypePairsInDatabase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var mimeTypes = context.MimeTypes.ToList();
                    var mimeSubtypes = context.MimeSubtypes.ToList();

                    var mediaTypesPairs = mediatypesJson
                        .Select(p => new MimeTypePair
                        {
                            MimeType = mimeTypes.FirstOrDefault(m => m.Name == p.MimeType),
                            MimeSubtype = mimeSubtypes.FirstOrDefault(s => s.Name == p.MimeSubtype)
                        })
                        .ToArray();

                    context.MimeTypePairs.AddOrUpdate(mediaTypesPairs);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportFileExtensionsToDatabase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var fileExtension = new HashSet<string>(mediatypesJson.Select(e => e.Extension))
                    .Select(e => new FileExtension
                    {
                        Name = e
                    })
                    .ToArray();

                    context.FileExtensions.AddOrUpdate(fileExtension);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportMimeSubtypesToDataBase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var mimeSubtypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeSubtype))
                    .Select(s => new MimeSubtype
                    {
                        Name = s
                    })
                    .ToArray();

                    context.MimeSubtypes.AddOrUpdate(mimeSubtypesNames);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }

        private async Task ImportMimeTypesToDatabase(ExtensionJson[] mediatypesJson)
        {
            try
            {
                using (var context = this.contextProvider.Create())
                {
                    var mimeTypesNames = new HashSet<string>(mediatypesJson.Select(t => t.MimeType))
                    .Select(m => new MimeType
                    {
                        Name = m
                    })
                    .ToArray();

                    context.MimeTypes.AddOrUpdate(mimeTypesNames);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                this.exceptions.Enqueue(e);
            }
        }
    }
}